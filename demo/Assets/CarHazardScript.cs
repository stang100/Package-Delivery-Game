using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHazardScript : MonoBehaviour
{
    public enum AIState
    {
        Stationary,
        Moving,
        Middle,
    }

    public AIState aiState;
    public GameObject gameObject;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.Stationary;
    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case AIState.Stationary:
                animator.Play("Still Car", 0, 0.0f);
            break;

            case AIState.Moving:
                animator.Play("Moving Car 0", 0, 0.0f);
                aiState = AIState.Middle;
            break;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (aiState == AIState.Stationary)
            {
                aiState = AIState.Moving;
            }

        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            aiState = AIState.Stationary;
        }
    }
}
