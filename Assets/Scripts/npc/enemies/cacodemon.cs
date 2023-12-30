using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class cacodemon : Enemy
{
   
     
     
    private bool isMoving = true;
 
    public int damageAmount = 5;
    private GameObject emptyTarget;
    bool chasingPlayer = false;
   public bool debug = true;
    [SerializeField] [Range(0f,100f)]
    float minDist = 20f;

 
    
    
    [Button("Test Damage")]
    public void testDamage()
    {
       
        damage(50);
    }


    public override void damage(int damageAmount, bool affectPhysichs = false, bool instantDead = true)
    {
        base.damage(damageAmount, affectPhysichs);
         
    }
    private  void Start()
    {
       /* anim = GetComponentInChildren<Animator>();
         emptyTarget = new GameObject();
        //target = emptyTarget.transform;
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        // target.position = spawner.getNewEmptyTarget();
        target = FindObjectOfType<player>().transform ;
        chasingPlayer = true;*/

        anim.SetTrigger("chasing");

    }
   
    
    private void OnDrawGizmos()
    {
        
    }
    private void FixedUpdate()
    {
         
      /* if(target)
        {
            Vector3 relPosition = target.position - transform.position;

            Quaternion q = Quaternion.LookRotation(relPosition);
            Quaternion q2 = Quaternion.Euler(transform.rotation.eulerAngles.x, q.eulerAngles.y, transform.rotation.eulerAngles.z);

            transform.rotation = q2;
 
            rb.velocity = transform.forward * speed*(chasingPlayer?2:1);
            distance = Vector3.Distance(transform.position, target.position);
            if (distance<minDist)
            {
                if (!chasingPlayer)
                {
                    target.position = spawner.getNewEmptyTarget();
                }
                else
                {
                   
                     AttackPlayer(damageAmount);
                }
            }
        }*/
    }
    public override void Attack()
    {
        base.Attack();
        AttackPlayer(damageAmount);
    }
    private void AttackPlayer(int damagef)
    {
        if (player.Instance)
        {
            if (!player.Instance.ComboMgr.isCombing)
                player.Instance.playerHealthManager.callUpdateHealth(damagef);
            damage(maxHealth + 1);
        }
    }


    void OnDestroy()
    {
        if (LevelManager.Instance)
        {
            LevelManager.Instance.UpdateTheScore(500);
        }
    }


}
 
 
