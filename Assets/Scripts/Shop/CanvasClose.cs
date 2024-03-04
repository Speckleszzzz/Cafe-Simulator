using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasClose : MonoBehaviour
{
    public GameObject canvasObject;
    public MonoBehaviour scriptToEnable;

    // Method to close the canvas and enable the specified script
    public void CloseCanvasAndEnable()
    {
        canvasObject.SetActive(false); // Close the canvas
        scriptToEnable.enabled = true; // Enable the specified script
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false;
    }
}