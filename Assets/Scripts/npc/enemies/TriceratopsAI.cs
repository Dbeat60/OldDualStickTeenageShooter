using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
public class TriceratopsAI : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent agent;
    public Animator anim;
    public float dist;
    public float lowSpeed;
    public float maxspeed;
    public float chaseDist = 25f;
    public float attackDist = 4f;
    public float cooloffAttack = 3f;
    WaitForSeconds waitTime ;
    [MinMaxSlider(0f,100f)]
    public  Vector2 minMaxSpeed ;

    bool isAttacking = false;
    bool canAttack = true;

    IEnumerator cooloffBaseAttack()
    {
        yield return waitTime;
        canAttack = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (agent && Target)
        {
            agent.destination = Target.position;
            lowSpeed = Random.Range(minMaxSpeed.x,minMaxSpeed.y);
            agent.speed = lowSpeed;
            maxspeed = lowSpeed+((minMaxSpeed.x+minMaxSpeed.y))/ 2f;
            waitTime = new WaitForSeconds(cooloffAttack);
        }
    }

    // Update is called once per frame
    void Update()
    {
         if(anim&&agent)
        {
            if (!isAttacking)
            {
                anim.SetFloat("speed", agent.velocity.magnitude);
            }
            dist = Vector3.Distance(transform.position, Target.position);
            anim.SetFloat("distToPlayer", dist);
            if (!isAttacking && canAttack)
            {
                if(dist <attackDist )
                {
                    isAttacking = true;
                    canAttack = false;
                    anim.SetTrigger("Attack");
                    anim.SetFloat("speed", 0);
                    agent.speed = 0f;
                    agent.enabled = false;

                }
                else if (dist < chaseDist)
                {
                    agent.speed = maxspeed;
                    agent.acceleration = maxspeed + 5f;
                }
                else
                {
                    agent.speed = lowSpeed;
                    agent.acceleration = lowSpeed + 5f;
                }
            }
           if(agent.enabled)
            agent.destination = Target.position;


        }
    }

    public void endAtack()
    {
        isAttacking = false;
        agent.enabled = true;
        StartCoroutine(cooloffBaseAttack());
    }
}
