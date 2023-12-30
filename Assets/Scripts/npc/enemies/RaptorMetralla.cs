using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RaptorMetralla : Enemy
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
    float inRangeDistance = 15f;
    bool canShoot = true;
    WaitForSeconds cooloffDelay = new WaitForSeconds(3f);
    [SerializeField]
    Transform shootpos1, shootpos2;
    [SerializeField]
    GameObject shootEffect;

 
    bool dead;
    // Start is called before the first frame update

     void shootDualGun()
    {
        if (!PoolSystem.Instance)
        {
            Instantiate(player.Instance.poolManagerPrefab, Vector3.zero, Quaternion.identity);
        }

        GameObject b1 = PoolSystem.Instance.getFromPool(false);
        b1.transform.position = shootpos1.position;
        b1.transform.rotation = shootpos1.rotation;
        b1.GetComponent<ProjectileAC>().shoot(shootpos1.forward * bulletSpeed);
        Instantiate(shootEffect, shootpos1.position, Quaternion.identity);

        GameObject b2 = PoolSystem.Instance.getFromPool(false);
        b2.transform.position = shootpos2.position;
        b2.transform.rotation = shootpos2.rotation;
        b2.GetComponent<ProjectileAC>().shoot(shootpos2.forward * bulletSpeed);
        Instantiate(shootEffect, shootpos2.position, Quaternion.identity);
    }
   
    public override void Attack()
    {
        if (canShoot)
        {

            PreparingAttack = true;
            canMove = false;

        }
       

    }
    public override void stopAttack()
    {
        if (canShoot)
        {
            agent.speed = speed;
            playerOnSight = false;
            anim.SetTrigger("aimEnd");
        }
    }
    private new void  Update()
    {
        base.Update();
        if(PreparingAttack)
        {
            lookForShootingTarget();
        }
    }

    public void lookForShootingTarget()
    {
        if(getDistanceToCurrentTarget()>fov)
        {
            PreparingAttack = false;
            canMove = true;
        }
         lookAtY();
        Ray ray = new Ray(fovPosition.position, fovPosition.forward);
        Debug.DrawLine(fovPosition.position, fovPosition.forward * fov, Color.red);
        RaycastHit hito;

        if (!playerOnSight)
        {
            anim.SetTrigger("aim");
        }
        playerOnSight = true;

        if (canShoot && Physics.Raycast(ray, out hito, fov))
        {

            if (hito.transform.tag == "Player")
            {
                 anim.SetTrigger("shoot");
                canShoot = false;
                StartCoroutine(shoot());

            }
            else
            {

            }
        }
    }
    // Update is called once per frame
    public override void damage(int damageAmount, bool affectPhysichs = false,bool instantDead=true)
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
        agent.enabled= false;
        canShoot = false;
        yield return new WaitForSeconds(3f);
        base.Dead();
    }
    IEnumerator shoot()
    {
        shootDualGun();
        yield return cooloffDelay;
        canShoot = true;
        canMove = true;
    }
        
}
