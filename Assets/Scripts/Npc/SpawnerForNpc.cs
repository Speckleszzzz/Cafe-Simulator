using UnityEngine;
using UnityEngine.AI;

public class SpawnerForNpc : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public Transform spawnPoint;
    public Transform pointB;
    public Transform destination;
    public GameObject dragObjectForNPC; // Reference to GameObject with DragObjectToBuy script

    private GameObject spawnedNPC;
    private NavMeshAgent agent;
    private bool hasReachedPointB = false;
    private bool hasReachedDestination = false;

    void Start()
    {
        SpawnNPC();
    }

    void SpawnNPC()
    {
        GameObject selectedNPCPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        spawnedNPC = Instantiate(selectedNPCPrefab, spawnPoint.position, Quaternion.identity);
        agent = spawnedNPC.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.SetDestination(pointB.position);
            agent.stoppingDistance = 0.1f;
            agent.autoBraking = true;
            agent.autoRepath = true;
            agent.SetAreaCost(0, 1f);
        }
    }

    void Update()
    {
        if (agent != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!hasReachedPointB)
            {
                hasReachedPointB = true;
                agent.SetDestination(destination.position);
            }
            else if (!hasReachedDestination)
            {
                hasReachedDestination = true;
                int randomNumber = Random.Range(1, 20); 
                Debug.Log("NPC has reached the destination. Random number generated: " + randomNumber);
                
                // Find DragObjectToBuy script on the specified GameObject
                DragObjectToBuy dragObjectToBuyScript = dragObjectForNPC.GetComponent<DragObjectToBuy>();
                if (dragObjectToBuyScript != null)
                {
                    dragObjectToBuyScript.SetNPCFoodId(randomNumber);
                }
                else
                {
                    Debug.LogError("DragObjectToBuy script not found on the specified GameObject!");
                }
            }
        }
    }

    public void DestroyNPC()
    {
        if (spawnedNPC != null)
        {
            Destroy(spawnedNPC);
        }
    }
}
