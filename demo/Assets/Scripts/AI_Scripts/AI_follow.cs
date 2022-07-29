using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_follow : MonoBehaviour
{
    private NavMeshAgent Mob;
    public GameObject Player;
    public float MobDistanceRun = 4.0f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < MobDistanceRun){
            Vector3 dirToPlayer  = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Mob.SetDestination(newPos);
            anim.Play("walk");
        }
        else{
            anim.Play("sit");
        }
    }
}
