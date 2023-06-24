using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Script for picking up objects in desktop view
public class PickupController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    [SerializeField] GameObject highlightedObj;
    private Rigidbody highlightedObjRB;
    public Material originalMaterial;
    public Material highlightedMaterial;
    private bool isPlaced;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    void Start()
    {
        originalMaterial = highlightedObj.GetComponent<Renderer>().material;
        highlightedObjRB = highlightedObj.GetComponent<Rigidbody>();
        StartCoroutine(DetermineHighlightLocation());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (heldObj == null) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange)) {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else {
                DropObject();
                highlightedObj.GetComponent<Renderer>().material = originalMaterial;
                highlightedObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (heldObj != null) {
            MoveObject();
            HighlightObject(heldObj);
        }
    }

    void PickupObject(GameObject pickObj) {
        if (pickObj.GetComponent<Rigidbody>()) {
            if (pickObj.gameObject.tag == "Seed" ||
                pickObj.gameObject.tag == "SeedQ1" ||
                pickObj.gameObject.tag == "SeedQ2" ||
                pickObj.gameObject.tag == "SeedQ3" ||
                pickObj.gameObject.tag == "SeedQ4" ||
                pickObj.gameObject.tag == "SeedQ5") {
                heldObjRB = pickObj.GetComponent<Rigidbody>();
                heldObjRB.useGravity = false;
                heldObjRB.drag = 10;
                heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                heldObjRB.transform.parent = holdArea;
                heldObj = pickObj;
            }
        }
    }

    void HighlightObject(GameObject pickObj) {
        if (pickObj.gameObject.tag == "Seed" && GameplayManager.instance.locIndex == 0)
        {
            highlightedObj = GameObject.Find("DirtQ1");
        } else if (pickObj.gameObject.tag == "Seed" && GameplayManager.instance.locIndex == 1)
        {
            highlightedObj = GameObject.Find("DirtQ2");
        } else if (pickObj.gameObject.tag == "Seed" && GameplayManager.instance.locIndex == 2)
        {
            highlightedObj = GameObject.Find("DirtQ3");
        } else if (pickObj.gameObject.tag == "Seed" && GameplayManager.instance.locIndex == 3)
        {
            highlightedObj = GameObject.Find("DirtQ4");
        } else 
        {
            highlightedObj = GameObject.Find("DirtQ5");
        }
        highlightedObj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if (highlightedObj.gameObject.tag == "Highlight") {
            highlightedObj.GetComponent<Renderer>().material = highlightedMaterial;
        }
    }

    void DropObject() {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;
    }

    void MoveObject() {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f) {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    IEnumerator DetermineHighlightLocation()
    {
        while (true)
        {
            if (heldObj != null && !isPlaced)
            {
                Debug.Log("HOLDING");
                //move highlightObj to appear in front of player

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object is the ground or suitable surface
                    if (hit.transform.CompareTag("Ground"))
                    {
                        // Set the position of the GameObject to the intersection point
                        highlightedObj.gameObject.transform.position = hit.point;
                        //highlightedObjRB.constraints = RigidbodyConstraints.FreezeAll;

                        // Disable the Rigidbody to prevent further movement, allows OnCollisionEnter to work
                        highlightedObjRB.isKinematic = true;
                        highlightedObjRB.useGravity = false;

                        isPlaced = true;
                    }
                }
            }
            if (GameplayManager.instance.planted)
            {
                isPlaced = false;
            }
            yield return null;
        }


    }
}
