using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleKnockback : MonoBehaviour
{
    public float damageKnockback;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponentInParent<Rigidbody>();
        Debug.Log("here");
        Debug.Log(collision.collider);
        if (rb != null)
        {
            Debug.Log("TOUCHED");
            Debug.Log(rb.gameObject.name);
            Vector3 direction = (rb.transform.position - transform.position).normalized;
            direction.z *= 2;
            direction.y /= 2;
            Vector3 knockback = direction * damageKnockback;
            Debug.Log(direction);

            //apply transformation to obj
            rb.AddForce(knockback, ForceMode.Impulse);
        }
    }
}
