using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
    public int buttonId; 
    private Button button;

    void Start()
    {
        button = GetComponent<Button>(); 
        UpdateButtonInteractability();
    }

    void Update()
    {
        // Update button interactability continuously
        UpdateButtonInteractability();
    }

    void UpdateButtonInteractability()
    {
        levelupstats levelUpStats = FindObjectOfType<levelupstats>();
        if (levelUpStats == null)
        {
            Debug.LogWarning("levelupstats script not found in the scene!");
        }
        else
        {
            Debug.Log("Current Level: " + levelUpStats.Level);
            Debug.Log("Button ID: " + buttonId);
            button.interactable = (levelUpStats.Level == buttonId);
            Debug.Log("Button Interactable: " + button.interactable);
        }
    }
}
