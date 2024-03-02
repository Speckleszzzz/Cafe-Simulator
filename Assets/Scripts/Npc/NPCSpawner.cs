using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public Transform[] initialWaypoints;
    public Transform[] finalWaypoints;
    private GameObject spawnedCharacter;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool reachedFirstWaypoint = false;
    private bool waitingForPlayerInput = false;

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

    void Update()
    {
        if (!reachedFirstWaypoint)
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
                    reachedFirstWaypoint = true;
                    waitingForPlayerInput = true;
                }
            }
        }
        else if (waitingForPlayerInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                waitingForPlayerInput = false;
                currentWaypointIndex = 0;
                MoveToNextWaypoint(finalWaypoints);
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
