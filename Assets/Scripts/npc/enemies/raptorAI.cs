using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class raptorAI : MonoBehaviour {

    public Animator Anim;
    public EnemyAI AI;
    private Rigidbody rb;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start ()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //TODO inherit from enemyAI
        AI = GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Anim)
        {
            Anim.SetFloat("speed", agent.velocity.magnitude);
            

        }
    }
}
