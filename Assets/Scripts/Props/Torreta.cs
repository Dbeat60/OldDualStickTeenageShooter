using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Torreta : Enemy
{
    public Transform Target;
    public Transform head;
    public Transform campoDeFuerza;
    public float shootSpeed=5f;
      public float maxLenght = 30f;
    bool canshoot= true;
    WaitForSeconds shootdelay, afterDeadTime ;
      
    public Transform shootPos;
    public GameObject shootEffect;
    public GameObject malfunctionEffect;
    public float bulletspeed = 250f;
    bool dead;
    IEnumerator shoot()
    {
        canshoot = false;

        anim.SetTrigger("shoot");

        if (!PoolSystem.Instance)
        {
            Instantiate(player.Instance.poolManagerPrefab, Vector3.zero, Quaternion.identity);
        }
        GameObject b = PoolSystem.Instance.getFromPool(false);
        b.transform.position = shootPos.position;
        b.transform.rotation = head.rotation;

        b.GetComponent<ProjectileAC>().shoot(head.forward * bulletspeed);

         
            Instantiate(shootEffect, shootPos.position, Quaternion.identity);
         

      //  Debug.Log("shoot");
        yield return shootdelay;
       
        canshoot = true;
    }
    // Sl6tart is called before the first frame update
    new void Start()
    {
 
        anim = GetComponent<Animator>();
        Target = FindObjectOfType<player>().transform;
        shootdelay = new WaitForSeconds(shootSpeed);
        afterDeadTime = new WaitForSeconds(5f);
    }
    public override void damage(int damageAmount, bool affectPhysichs = false, bool instantDead = true)
    {
        if (!dead)
        {
            float reductRate = (float)health / (float)maxHealth;

             campoDeFuerza.GetComponent<Renderer>().material.SetColor("Color_67DAFA1E", Color.Lerp(new Color(255f, 2f, 0f), new Color(0f, 28f, 191f), reductRate-0.1f));
            base.damage(damageAmount, affectPhysichs, false);
            if (health <= 0)
            {
                dead = true;
                canshoot = true;
                shootdelay = new WaitForSeconds(shootSpeed / 7.5f);
                StartCoroutine(dieT());
            }
        }
    }
    public Transform deadDestination;
    IEnumerator dieT()
    {
       Transform x = Instantiate(malfunctionEffect, damageParticlePosition.position, Quaternion.identity).transform;
        x.parent = transform;
        yield return afterDeadTime;
        
        base.Dead();

        
    }
   
    // Update is called once per frame
    void Update()
    {
        if(!dead && Target)
        {
            Vector3 relPosition = Target.position - transform.position;

            Quaternion q = Quaternion.LookRotation(relPosition);
            Quaternion q2 = Quaternion.Euler(head.rotation.eulerAngles.x, q.eulerAngles.y, head.rotation.eulerAngles.z);

            head.rotation = Quaternion.Slerp(head.rotation, q2,Time.deltaTime);
            if (canshoot)
            {
                Ray r = new Ray(head.position, head.forward);
                RaycastHit hito;
                if (Physics.Raycast(r, out hito, maxLenght))
                {
                    if (hito.transform.tag == "Player")
                    {
                        StartCoroutine(shoot());
                    }
                }
            }
        }
        else if(dead)
        {
            head.position +=( deadDestination.position - head.position)*speed;
            head.Rotate(head.up, 5f);
            if (canshoot)
            {
                 
                {
                     
                        StartCoroutine(shoot());
                     
                }
            }
        }
    }
}
