using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject[] shopPrefabs;
    public Transform spawnPoint;
    public Transform[] initialWaypoints;
    public Transform[] finalWaypoints;
    private GameObject spawnedCharacter;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool reachedFinalInitialWaypoint = false;
    private bool waitingForPlayerInput = false;
    private int desiredFoodId;
    private bool itemCorrect = false;
    public FoodManager foodManager; 
    public levelupstats levelUpStats; 

    void Start()
    {
        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        int randomIndex = Random.Range(0, characterPrefabs.Length);
        GameObject characterPrefab = characterPrefabs[randomIndex];
        spawnedCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        agent = spawnedCharacter.GetComponent<NavMeshAgent>();
        MoveToNextWaypoint(initialWaypoints);
    }

    void MoveToNextWaypoint(Transform[] waypoints)
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned!");
            return;
        }
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void SetDesiredFoodId(int foodId)
    {
        desiredFoodId = foodId;
    }

    public void NotifyItemCollected()
    {
        if (reachedFinalInitialWaypoint && waitingForPlayerInput)
        {
            currentWaypointIndex = 0;
            MoveToNextWaypoint(finalWaypoints);
            GiveExperience();
            waitingForPlayerInput = false;
        }
    }

    void GenerateDesiredItem()
    {
        int randomItemIndex = Random.Range(0, shopPrefabs.Length);
        GameObject selectedObject = shopPrefabs[randomItemIndex];
        FoodIdentifier foodIdentifier = selectedObject.GetComponent<FoodIdentifier>();
        if (foodIdentifier != null)
        {
            desiredFoodId = foodIdentifier.GetFoodId();
            Debug.Log("NPC wants food with ID: " + desiredFoodId);
        }
        else
        {
            Debug.LogError("Selected object does not have FoodIdentifier component!");
        }
    }
    public void CheckItemCorrectness(int broughtFoodId)
    {
        Debug.Log("Desired food ID: " + desiredFoodId);
        Debug.Log("Brought food ID: " + broughtFoodId);

        if (broughtFoodId == desiredFoodId)
        {
            itemCorrect = true;
            Debug.Log("Correct item brought!");
        }
        else
        {
            itemCorrect = false;
            Debug.Log("Incorrect item brought!");
        }
    }

    void GiveExperience()
    {
        float expGained = CalculateExperienceOnLeave();
        levelUpStats.SetExperience(expGained);
    }

    float CalculateExperienceOnLeave()
    {
        return 5.0f;
    }

    void Update()
    {
        if (!reachedFinalInitialWaypoint)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (currentWaypointIndex < initialWaypoints.Length - 1)
                {
                    currentWaypointIndex++;
                    MoveToNextWaypoint(initialWaypoints);
                }
                else
                {
                    reachedFinalInitialWaypoint = true;
                    waitingForPlayerInput = true;
                    GenerateDesiredItem();
                }
            }
        }
        else if (waitingForPlayerInput)
        {
            if (itemCorrect)
            {
                currentWaypointIndex = 0;
                MoveToNextWaypoint(finalWaypoints);
                GiveExperience();
                waitingForPlayerInput = false;
            }
            else
            {
                // Handle incorrect item brought
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (currentWaypointIndex < finalWaypoints.Length - 1)
                {
                    currentWaypointIndex++;
                    MoveToNextWaypoint(finalWaypoints);
                }
            }
        }
    }

    
}
