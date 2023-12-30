using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private Animator animator;
    public NavMeshAgent agent { private set; get; }

    [SerializeField]
    private Transform waypointManager;
    private Transform[] waypoints;

    [SerializeField]
    private int currentWP;
    [SerializeField]
    private float minDistance = 0.5f;
    [SerializeField]
    private float maxDist = 7.0f;
    private float distanceToTarget;
    private Transform target;
    [SerializeField]
    private bool hasWayPoints;

    public enum AIState
    {
        Patrol,
        Seeking,
        CoolingOff
    }
    public AIState state;

    [SerializeField]
    private bool updateAlways = true;

    [SerializeField]
    private float comradesRadius = 3.0f;
    public List<EnemyAI> comrades;
    public Collider[] comradesColliders;
    public float minSpeed;
    public float maxSpeed;

    [SerializeField]
    private float coolOffTimer = 2.5f;
    private float updateTargetRefresh = 0.2f;
    private float updateTargetTimer;

    private bool debug;

    // Use this for initialization
    void Start()

    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(minSpeed, maxSpeed);

        if (hasWayPoints
            && waypointManager != null)
        {
            waypoints = new Transform[waypointManager.childCount];
            for (int i = 0; i < waypointManager.childCount; i++)
            {
                waypoints[i] = waypointManager.GetChild(i);
            }

            FindNewTarget();
        }
        else
        {
            if (target == null)
            {
                if (player.Instance)
                {
                    target = player.Instance.transform;
                }
                PlayerFound(target);
            }
        }

        updateTargetTimer = updateTargetRefresh;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            if (waypoints.Length > 0)
            {
                foreach (Transform t in waypoints)
                {

                    Gizmos.DrawSphere(t.position, 1f);
                }
            }
            Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
            Gizmos.DrawSphere(transform.position, comradesRadius);
        }
    }

    public void PlayerFound(Transform playerTransform)
    {
        state = AIState.Seeking;
        target = playerTransform;
        comradesColliders = Physics.OverlapSphere(transform.position, comradesRadius);
        for (int c = 0; c < comradesColliders.Length; ++c)
        {
            EnemyAI enemyAI = comradesColliders[c] != null ? comradesColliders[c].GetComponent<EnemyAI>() : null;
            if (enemyAI != null
                && enemyAI.state == AIState.Patrol)
            {
                enemyAI.PlayerFound(playerTransform);
            }
        }
    }

    void Update()
    {
        if (state == AIState.Patrol)
        {
            if (updateAlways)
            {
                if (updateTargetRefresh > 0.0f)
                {
                    updateTargetRefresh -= Time.deltaTime;
                }
                else
                {
                    UpdateTarget();
                }
            }

            if (animator && agent)
            {
                animator.SetFloat("Speed", agent.speed);
            }

            distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= minDistance)
            {
                FindNewTarget();
            }
        }
        else if (state == AIState.Seeking)
        {
            if (updateAlways)
            {
                if (updateTargetRefresh > 0.0f)
                {
                    updateTargetRefresh -= Time.deltaTime;
                }
                else
                {
                    UpdateTarget();
                }
            }
            if (target)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);
            }
            if (distanceToTarget <= minDistance)
            {
                if (player.Instance)
                {
                    player.Instance.playerHealthManager.callUpdateHealth(15.0f);
                }
                if (animator)
                {
                    animator.SetTrigger("bite");
                }
                agent.isStopped = true;

                CoolOff();
            }
        }
    }

    private void FindNewTarget()
    {
        //currentWP = Random.Range(0, waypoints.Length - 1);
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        //target = waypoints[currentWP];
        agent.SetDestination(target.position);

        if (updateAlways)
        {
            updateTargetTimer = updateTargetRefresh;
        }
    }

    public void CoolOff()
    {
        if (state != AIState.CoolingOff)
        {
            StartCoroutine(CoolOffCoroutine());
        }
    }

    private IEnumerator CoolOffCoroutine()
    {
        state = AIState.CoolingOff;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        yield return new WaitForSeconds(coolOffTimer);
        if (agent)
        {
            agent.isStopped = false;
        }
        state = AIState.Seeking;
    }

}