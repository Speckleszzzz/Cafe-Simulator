using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public string itemName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Foods"))
        {
            string itemNameEntering = other.gameObject.name;
            string desiredItemName = itemName;
            NPCSpawner spawner = FindObjectOfType<NPCSpawner>(); 
            
            Debug.Log("Desired Item Name: " + desiredItemName);
            Debug.Log("Entering Item Name: " + itemNameEntering);
            
            if (spawner != null && desiredItemName.Equals(itemNameEntering, System.StringComparison.OrdinalIgnoreCase)) 
            {
                spawner.NotifyItemCollected(); 
                
                // Destroying the object entering the collider
                Destroy(other.gameObject); 
                
                Debug.Log("Correct item collected: " + desiredItemName + " (Entering: " + itemNameEntering + ")");
            }
            else
            {
                Debug.Log("Incorrect item collected: " + desiredItemName + " (Entering: " + itemNameEntering + ")");
            }
        }
    }
}
