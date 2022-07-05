using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarAI_control : MonoBehaviour
{
    private Animator anim;	
    private NavMeshAgent agent;
    public GameObject[] waypoints;
    int currWaypoint;
    float distance;
    float lookaheadTime;


    public enum AIState {
        statWayPoints
        //TODO more? states...
        };
        
    public AIState aiState;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        // movingPoint = GameObject.Find("Cube");
        // velReporter = movingPoint.GetComponent<VelocityReporter>();
        
        currWaypoint = -1;
        setNextWaypoint();
        aiState = AIState.statWayPoints;
        
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        // Debug.Log(movingPoint.transform.position);
        switch (aiState){
            case AIState.statWayPoints:
            Debug.Log(agent.remainingDistance-agent.stoppingDistance);
                if (!agent.pathPending && agent.remainingDistance-agent.stoppingDistance==0){
                    setNextWaypoint();
                }
                break;
        }

        
    }

    private void setNextWaypoint(){
        currWaypoint = currWaypoint + 1;
        if (currWaypoint >= waypoints.Length || waypoints.Length == 0){
            currWaypoint = -1;
            // aiState = AIState.movingWayPoint;
        }
        else{
            if (waypoints.Length != 0){
                agent.SetDestination(waypoints[currWaypoint].transform.position);
        }

        }
        Debug.Log(agent.destination);

    }
}
