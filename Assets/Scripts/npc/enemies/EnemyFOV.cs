using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {


    [SerializeField]
    private Transform head;

    [SerializeField]
    [Range(1, 360)]
    private int minAngle;
    [SerializeField]
    [Range(1, 360)]
    private int maxAngle;
    [SerializeField][Range (1,360)]
    private int angleIncrease;
    [SerializeField]
    private float yOffset;
     [SerializeField]
    private bool debug = true;
    private EnemyAI enemyAI;

    public float Dist=0;
    public float ViewLength;


    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update ()
    {
        if(enemyAI.state == EnemyAI.AIState.Patrol)
        for (int i = minAngle; i < maxAngle; i+=angleIncrease)
        {
            Vector3 v = transform.right ;
            v= Quaternion.AngleAxis(i, transform.up)*v;
            v.Normalize();            
            Ray r = new Ray(head.position, v);            
            RaycastHit hit;

            if (debug)
            {
                Debug.DrawRay(head.position, v * ViewLength, Color.red);
            }
            if (Physics.Raycast(r, out hit,ViewLength))
                {
                    if (hit.transform.tag == "Player")
                    {
                        enemyAI.PlayerFound(hit.transform);
                    }
            }
                
        }
       
       
      
	}
}
