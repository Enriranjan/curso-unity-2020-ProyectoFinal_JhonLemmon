using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform[] waypoint;

    private int currentWaypointIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(waypoint[currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoint.Length;
            _navMeshAgent.SetDestination(waypoint[currentWaypointIndex].position); 
        }
    }
}
