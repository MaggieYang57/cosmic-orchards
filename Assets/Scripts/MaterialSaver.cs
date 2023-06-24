using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSaver : MonoBehaviour
{
    Material originalMat;

    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<Renderer>().material;
        Debug.Log(originalMat);

    }

    public void Restore()
    {
        GetComponent<Renderer>().material = originalMat;
    }
}
