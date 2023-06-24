using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarChime : MonoBehaviour
{
    public AudioSource chime;

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
            if (GameplayManager.instance.callShootingStar == true && GameplayManager.instance.playedChime == false)
            {
                yield return new WaitForSeconds(2f);
                chime.Play();
                GameplayManager.instance.playedChime = true; //changed to false again in throwAnimation
            }
            yield return null;
        }
    }
}
