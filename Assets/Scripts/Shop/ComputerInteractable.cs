using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerInteractable : MonoBehaviour
{
    public GameObject canvasObject;
    public MonoBehaviour scriptToDisable;
    public string interactionKey = "e"; // Key to interact with the object

    private bool isLookingAtObject = false;
    private bool isCanvasActive = false;

    private void Update()
    {
        // Check if the player is looking at the object
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Object is being looked at
                isLookingAtObject = true;
            }
            else
            {
                // Object is not being looked at
                isLookingAtObject = false;
            }
        }
        else
        {
            // No hit, object is not being looked at
            isLookingAtObject = false;
        }

        // Check for interaction key press
        if (Input.GetKeyDown(interactionKey))
        {
            if (isLookingAtObject)
            {
                if (!isCanvasActive)
                {
                    // Activate canvas and disable script
                    ActivateCanvas();
                }
                else
                {
                    // Deactivate canvas
                    DeactivateCanvas();
                }
            }
        }
    }

    private void ActivateCanvas()
    {
        canvasObject.SetActive(true);
        scriptToDisable.enabled = false;
        isCanvasActive = true;
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Show cursor
    }

    private void DeactivateCanvas()
    {
        canvasObject.SetActive(false);
        scriptToDisable.enabled = true;
        isCanvasActive = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }
}