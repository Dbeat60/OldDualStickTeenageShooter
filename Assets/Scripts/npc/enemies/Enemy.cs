using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {

    [SerializeField]
    private GameObject DeadEffect;
    [SerializeField]
    private GameObject damageEffect;

    public EnemySpawner spawner;
    public Transform damageParticlePosition;
   

    
    [SerializeField]
    protected float distance;
    [SerializeField][Range(0f, 100f)]
    private float attackRangeDistance = 15f;
    private Transform target;   
    [SerializeField] [Range(0f, 100f)]
    private float rotationSpeed = 3f;
    [SerializeField]
    private bool isStatic = false;


    protected NavMeshAgent agent;
    [SerializeField]
    [Range(0f, 250f)]
    protected float speed = 5f;
    protected int health;
    protected bool canAtack = true;
    [SerializeField] protected bool AttackPlayerAutomatic;
    protected Animator anim;
    [SerializeField]
    protected int maxHealth = 100;
    protected bool PreparingAttack = false;
    protected bool canMove = true;
    protected Rigidbody rb;
    [SerializeField]
    Transform debugTarget;
    [SerializeField] bool useDebugTarget;
    public enum EnemyState
    {
        Idle,
        Wandering,
        Searching,
        Attack
    }
    public EnemyState CurrentState;
    public void Awake()
    {
        PreparingAttack = false;
        //Debug.Log("start");
        if (!isStatic)
        {

            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            if (useDebugTarget && debugTarget)
            {
                setTarget(debugTarget);
            }
            else
            {
                setTarget(FindObjectOfType<player>().transform);
            }
            if (!target && AttackPlayerAutomatic)
            {
                Destroy(gameObject);
            }
            else
            {
                CurrentState = EnemyState.Searching;
            }
        }
        else
        {
            CurrentState = EnemyState.Idle;
        }
        anim = GetComponentInChildren<Animator>();
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        

    }
    // Use this for initialization
    public virtual  void damage(int damageAmount, bool affectPhysichs=false, bool instantDead=true)
    {
        Instantiate(damageEffect,(damageParticlePosition?damageParticlePosition.position: transform.position), Quaternion.identity);
        print("damage "+ transform.name);
        if(affectPhysichs)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * damageAmount , ForceMode.Impulse);
            StartCoroutine(resetphysics());
        }
        health -= damageAmount;

        if (instantDead&& health <= 0)
        {
            Dead();
        }
    }
    IEnumerator resetphysics()
    {
        yield return new WaitForSeconds(0.351f);
        if (rb)
        {
            rb.velocity = Vector3.zero;
        }
    }
    public int getHealth()
    {
        return health;
    }
    public void setTarget(Transform target)
    {
        this.target = target;
    }
    public void lookAtY()
    {
        if (target)
        {
            agent.speed = 0;
            Vector3 relPosition = target.position - transform.position;

            Quaternion q = Quaternion.LookRotation(relPosition);
            Quaternion q2 = Quaternion.Euler(transform.rotation.eulerAngles.x, q.eulerAngles.y, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime *
                rotationSpeed);
        }

    }
    public void GoTowardsTarget()
    {
         
        if(agent && agent.enabled)
        {
            distance = getDistanceToCurrentTarget();
            if (distance < attackRangeDistance)
            {
                if (canAtack)
                {
                    Attack();
                }




            }
            else if(canMove)
            {
                agent.speed = speed;
                agent.SetDestination(target.position);
                print("going");
            }
            anim.SetFloat("speed", agent.speed);

        }

    }
    public float getDistanceToCurrentTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    public virtual void stopAttack()
    {
       
    }
    public virtual void Move()
    {
        GoTowardsTarget();
    }
    public virtual void Attack()
    {
        CurrentState = EnemyState.Attack;
    }
    public void Dead()
    {
        if (DeadEffect != null)
        {
            Instantiate(DeadEffect, (damageParticlePosition ? damageParticlePosition.position : transform.position), Quaternion.identity);
        }
        Destroy(gameObject,0.21f);
    }

    public void Update()
    {
        if (!isStatic)
        {
            Move();
        }
    }
}
