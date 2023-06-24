using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSeedChime : MonoBehaviour
{
    public AudioSource chime;
    public bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CallChime());
    }

    // Update is called once per frame
    IEnumerator CallChime()
    {
        while (true)
        {
            if (GameplayManager.instance.numPlanted == 5 && !played)
            {
                chime.Play();
                played = true; //changed to false again in throwAnimation
            }
            yield return null;
        }
    }
}
