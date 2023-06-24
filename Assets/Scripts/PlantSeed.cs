
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObject plant;
    public GameObject gizmoAppear;
    public GameObject gizmoDisappear;
    public float speed = 2F;
    public string dystopiaQuadrantTag;
    public string forestQuadrantTag;
    public Material natureTransitionMaterial;
    public Material dystopianTransitionMaterial;
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    private bool localPlanted;


    void Start()
    {
        gizmoDisappear.transform.position = new Vector3(gizmoDisappear.transform.position.x, 40, gizmoDisappear.transform.position.z);
        gizmoAppear.transform.position = new Vector3(gizmoAppear.transform.position.x, 0, gizmoAppear.transform.position.z);
        GameObject[] forestObjects = GameObject.FindGameObjectsWithTag(forestQuadrantTag);

        foreach (GameObject obj in forestObjects) 
        {
            Transform[] children = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material originalMaterial = renderer.material;
                    originalMaterials[obj] = originalMaterial;
                    Debug.Log(originalMaterials[obj]);
                    renderer.material = natureTransitionMaterial;
                    renderer.enabled = false;
                }
            }
        }
    }

    void Update()
    {
        GameObject[] dystopiaObjects = GameObject.FindGameObjectsWithTag(dystopiaQuadrantTag);
        GameObject[] forestObjects = GameObject.FindGameObjectsWithTag(forestQuadrantTag);

        if (localPlanted) {
            if (gizmoDisappear.transform.position.y > 0)
            {
                // Change material of dystopia objects and make them disappear
                foreach (GameObject obj in dystopiaObjects) {
                    Transform[] children = obj.GetComponentsInChildren<Transform>(true);
                    foreach (Transform child in children)
                    {
                        Renderer renderer = child.GetComponent<Renderer>();
                        if (renderer != null) {
                            renderer.material = dystopianTransitionMaterial;
                        }
                    }
                }
                StartCoroutine(disappear());
            }
            else
            {   
                // Disable dystopia objects and make forest objects appear
                foreach (GameObject obj in dystopiaObjects) {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in forestObjects) 
                {
                    Transform[] children = obj.GetComponentsInChildren<Transform>(true);
                    foreach (Transform child in children)
                    {
                        Renderer renderer = child.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.enabled = true;
                        }
                    }
                }

                StartCoroutine(appear());
            }
        }
    }

    IEnumerator disappear()
    {
        if (gizmoDisappear.transform.position.y > 0)
        {
            gizmoDisappear.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        yield return null;
    }

    IEnumerator appear()
    {
        GameObject[] forestObjects = GameObject.FindGameObjectsWithTag(forestQuadrantTag);

        if (gizmoAppear.transform.position.y < 40)
        {
            gizmoAppear.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Reset position of gizmos
            gizmoDisappear.transform.position = new Vector3(gizmoDisappear.transform.position.x, 40, gizmoDisappear.transform.position.z);
            gizmoAppear.transform.position = new Vector3(gizmoAppear.transform.position.x, 0, gizmoAppear.transform.position.z);
            // Reset materials of forest objects
            RestoreMaterials();
            GameplayManager.instance.planted = false;
            localPlanted = false;

            // only call next star after animation finishes
            if (GameplayManager.instance.changeTargetLocation == false)
            {
                GameplayManager.instance.changeTargetLocation = true;
            }
            if (GameplayManager.instance.callShootingStar == false)
            {
                GameplayManager.instance.callShootingStar = true;
            }

        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Seed")
        {
            Debug.Log("Planted seed");
            GameplayManager.instance.planted = true;
            localPlanted = true;
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameplayManager.instance.numPlanted++;

            //modified to fit grass_simple2 model
            GameObject p = Instantiate(plant, transform.position, Quaternion.identity);
            p.transform.localScale -= new Vector3(0.5f,0.5f,0.5f);
            Debug.Log(p.transform.position);
        }
    }

    // Function to restore the original materials
    public void RestoreMaterials()
    {
        foreach (KeyValuePair<GameObject, Material> pair in originalMaterials)
        {
            GameObject obj = pair.Key;
            Material originalMaterial = pair.Value;

            Transform[] children = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = originalMaterial;
                }
            }
        }
    }
}
