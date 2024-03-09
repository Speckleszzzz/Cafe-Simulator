using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
    public int buttonId; // Unique identifier for this button
    private Button button;

    void Start()
    {
        button = GetComponent<Button>(); // Get the Button component attached to this GameObject

        // Check if levelupstats script is present
        levelupstats levelUpStats = FindObjectOfType<levelupstats>();
        if (levelUpStats == null)
        {
            Debug.LogWarning("levelupstats script not found in the scene!");
        }
        else
        {
            // Disable the button if the level doesn't match the buttonId
            Debug.Log("Current Level: " + levelUpStats.Level);
            Debug.Log("Button ID: " + buttonId);
            button.interactable = (levelUpStats.Level == buttonId);
            Debug.Log("Button Interactable: " + button.interactable);
        }
    }
}
