using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrabable objectGrabable;
    private Collider playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider>(); // Get the collider of the player
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabable == null)
            {
                float pickupDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabable))
                    {
                        // Disable collisions between the player and the grabbed object
                        Collider objectCollider = raycastHit.collider;
                        if (objectCollider != null)
                        {
                            Physics.IgnoreCollision(playerCollider, objectCollider, true);
                        }

                        objectGrabable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                objectGrabable.Release();
                objectGrabable = null;
            }
        }
    }
}
