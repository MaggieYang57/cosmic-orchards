using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryLogic : MonoBehaviour
{
    /*private void OnCollisionExit(Collision other)
    {
        // Check if the game object has entered the boundary
        if (other.gameObject.CompareTag("Player")) // Replace "Player" with the tag of your game object
        {
            Debug.Log("crossed");
            // Prevent the game object from passing through the boundary
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }*/
    private bool isInsideMeshCollider = true;

    private Vector3 previousPosition;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("stored");
            previousPosition = transform.position;
            isInsideMeshCollider = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ITS LEAVING");
            isInsideMeshCollider = false;
        }
    }
    private void Update()
    {
        if (!isInsideMeshCollider)
        {
            // Check if the GameObject's position is outside the mesh collider
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            Bounds colliderBounds = meshCollider.bounds;
            Vector3 playerPosition = transform.position;

            Debug.Log("UHHH: " + meshCollider);
            if (!colliderBounds.Contains(playerPosition))
            {
                // Move the GameObject back inside the mesh collider
                Vector3 closestPointOnCollider = colliderBounds.ClosestPoint(playerPosition);
                transform.position = closestPointOnCollider;
            }
        }
    }
}
