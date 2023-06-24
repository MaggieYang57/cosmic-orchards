using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HighlightObject : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Highlight()
    {
        GetComponent<Renderer>().material = highlightedMaterial;
    }

    public void UnHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}
