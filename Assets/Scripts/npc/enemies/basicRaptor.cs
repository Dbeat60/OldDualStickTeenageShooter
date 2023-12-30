using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class basicRaptor : Enemy
{  
    private bool isMoving = true;
    public int damageAmount = 5;
    private GameObject emptyTarget;
    bool chasingPlayer = false;
    public bool debug = true;
    [SerializeField]
    [Range(0f, 100f)]
    float minDist = 20f;

    [SerializeField]
    Transform fovPosition;
    [SerializeField]
    [Range(0f, 100f)]
    float fov = 10f;
    
    
    bool playerOnSight;
    [SerializeField]
    [Range(0f, 2500f)]
    float bulletSpeed = 205f;
    [SerializeField]
    [Range(0f, 100f)]
    WaitForSeconds jumpCoolOffDuration = new WaitForSeconds(3f);
    WaitForSeconds attackDelay = new WaitForSeconds(2.5f);

 
    bool dead;
    [SerializeField][Range(0,1500)]
    float jumpSpeed = 25f;
    // Start is called before the first frame update
     
    IEnumerator jumpAttack()
    {
        PreparingAttack = true;
        canAtack = false;
        yield return attackDelay;
        PreparingAttack = false;
        anim.SetBool("prepareJump", false);
        anim.SetTrigger("jump");
        rb.AddForce((transform.up + transform.forward).normalized * jumpSpeed, ForceMode.Impulse);
        canAtack = false;
        
        
    }
    public void endAttack()
    {
        agent.speed = base.speed;
        canAtack = true;

    }
    public override void Attack()
    {
        print("ataca");
        if (!PreparingAttack)
        {
            anim.SetTrigger("jump");
            PreparingAttack = true;
        }
        canMove = false;
    }
    new void Update()
    {
        base.Update();
        if(PreparingAttack)
        {
            lookAtY();
            
        }
    }
    // Update is called once per frame
     
    public override void damage(int damageAmount, bool affectPhysichs = false, bool instantDead = true)
    {
        if (!dead)
        {
            base.damage(damageAmount, affectPhysichs, false);
            anim.SetTrigger("damage");
            if (health <= 0)
            {
                dead = true;
                deadR();
            }
        }
    }
    public void deadR()
    {
        anim.SetBool("dead", true);
        StartCoroutine(deadC());
    }
    IEnumerator deadC()
    {
        agent.enabled = false;
        canAtack = false;
        yield return new WaitForSeconds(3f);
        base.Dead();
    }
    IEnumerator coolOffJump()
    {
         
        yield return jumpCoolOffDuration;
        canMove = true;
        canAtack = true;
        
    }
    public void PrepareJump()
    {
        Debug.Log("prepare jump");
    }
    public void JumpAndAttack()
    {
        Debug.Log(" jump");
        rb.AddForce((transform.forward + transform.up) * 0.5f * jumpSpeed, ForceMode.Impulse);

    }
    public void EndJump()
    {
        Debug.Log("landed jump");
        PreparingAttack = false;

        StartCoroutine(coolOffJump());
        rb.velocity = Vector3.zero;

    }
}
