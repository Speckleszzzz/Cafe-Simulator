using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints; 

    void Start()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[0].position);
        }
        else
        {
            Debug.LogWarning("No waypoints defined for the NPC to follow.");
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            int nextWaypointIndex = (agent.pathEndPosition == waypoints[waypoints.Length - 1].position) ? 0 : (agent.pathEndPosition == waypoints[0].position) ? 1 : -1;

            if (nextWaypointIndex != -1)
            {
                agent.SetDestination(waypoints[nextWaypointIndex].position);
            }
        }
    }
}
