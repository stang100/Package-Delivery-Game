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
    private AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Audio == null){
            Debug.Log("CANNOT FIND AUDIO");
        }

        if (distance < MobDistanceRun){
            Vector3 dirToPlayer  = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Mob.SetDestination(newPos);
            anim.Play("walk");
            if (Audio.isPlaying == false){
                Audio.Play();
                
            }
            
        }
        else{
            anim.Play("sit");
            Audio.Stop();
        }
    }
}


