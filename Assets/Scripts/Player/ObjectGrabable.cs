using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private CharacterController characterController; // Reference to the CharacterController

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component if it exists
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = true;

        // Check if CharacterController exists before disabling it
        if (characterController != null)
        {
            characterController.enabled = false;
        }
    }

    public void Release()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;

        // Check if CharacterController exists before enabling it
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
