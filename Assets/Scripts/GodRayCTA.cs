using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GodRayCTA : MonoBehaviour
{
    [SerializeField] private float fadePerSecond = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeGodRay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeGodRay()
    {
        GetComponent<Renderer>().enabled = false;
        var material = GetComponent<Renderer>().material;

        while (true)
        {
            yield return new WaitUntil(() => GameplayManager.instance.callGodRay == true);

            //determine placement in front of character
            //gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane))
            //  + (Camera.main.transform.forward*7) + new Vector3(0,12,0);

            //OR placement at landing spot
            gameObject.transform.position = GameObject.Find("StarTarget").transform.position;
            //fade in
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, 0);
            yield return new WaitForSeconds(1f);
            GetComponent<Renderer>().enabled = true;
            while (material.color.a < 1)
            {
                color = material.color;
                material.color = new Color(color.r, color.g, color.b, color.a + (fadePerSecond * Time.deltaTime));
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1.0f);
            //fade out
            while (material.color.a > 0)
            {
                color = material.color;
                material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
                yield return new WaitForSeconds(0.05f);
            }
            GetComponent<Renderer>().enabled = false;

            GameplayManager.instance.callGodRay = false;
        }
    }
}
