using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Items_Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private int destinationPoint = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        agent.destination = points[destinationPoint].position;

        destinationPoint++;
        if(destinationPoint >= points.Length)
        {
            destinationPoint = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }
}
