using UnityEngine;

public class DragObjectToBuy : MonoBehaviour
{
    private int npcFoodId;
    private Collider colliderComponent; 
    public levelupstats LevelUpStats;
    public SpawnerForNpc spawnerForNpc;
    

    private void Start()
    {
        colliderComponent = GetComponent<Collider>();
        colliderComponent.enabled = false;
    }

    public void SetNPCFoodId(int foodId)
    {
        npcFoodId = foodId;
        colliderComponent.enabled = true;
        Debug.Log("NPC food ID set to: " + foodId);
        Debug.Log("Collider enabled: " + colliderComponent.enabled);
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodIdentifier idScript = other.GetComponent<FoodIdentifier>();
        if (idScript != null)
        {
            if (idScript.foodId == npcFoodId)
            {
                Debug.Log("NPC generated number and object ID match. Object with ID " + idScript.foodId + " entered the collider and will be destroyed.");
                Destroy(other.gameObject);
                LevelUpStats.SetExperience(5.0f); 
                spawnerForNpc.DestroyNPC();
            }
            else
            {
                Debug.Log("NPC generated number and object ID do not match. Object with ID " + idScript.foodId + " entered the collider.");
            }
        }
    }
}