using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public int itemId;
    public NPCSpawner spawner; // Reference to NPCSpawner

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Foods"))
        {
            int itemIdEntering = other.gameObject.GetComponent<FoodIdentifier>().GetFoodId();

            Debug.Log("Desired Item ID: " + itemId);
            Debug.Log("Entering Item ID: " + itemIdEntering);

            if (spawner != null && itemId == itemIdEntering)
            {
                spawner.CheckItemCorrectness(itemIdEntering);
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Incorrect item collected!");
            }
        }
    }
}
