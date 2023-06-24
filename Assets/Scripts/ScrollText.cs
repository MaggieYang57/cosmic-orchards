using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float scrollSpeed = 30;

    void Start()
    {
        StartCoroutine(scrollText());
    }

    IEnumerator scrollText()
    {
        while (true)
        {

            if (GameplayManager.instance.numPlanted == 5)
            {
                yield return new WaitForSeconds(16.0f); // delay for final transition to occur

                while (gameObject.transform.position.y <= Screen.height + 200)
                {
                    gameObject.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime, Space.World);
                    yield return null;
                }
            }

            yield return null;
        }
        
    }
}
