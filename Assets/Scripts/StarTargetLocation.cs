using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTargetLocation : MonoBehaviour
{
    private void Update()
    {
        GeneratePoint();
    }
    void GeneratePoint()
    {
        if (GameplayManager.instance.changeTargetLocation == true && GameplayManager.instance.locIndex < 4)
        {
            gameObject.transform.position = GameplayManager.instance.nextLocation[GameplayManager.instance.locIndex];
            GameplayManager.instance.locIndex++;

            GameplayManager.instance.changeTargetLocation = false;
        }
    }

}