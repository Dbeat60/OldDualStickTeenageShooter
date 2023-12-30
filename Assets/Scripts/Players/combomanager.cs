using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combomanager : MonoBehaviour {

	// Use this for initialization
	 
    private bool isReading;
    private bool comboTimeOver;
    private bool attacking;
   public bool isCombing;
    [SerializeField]
    private float sphereRadius = 1f;
    [SerializeField]
    private Vector3 sphereOffset;
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private GameObject batEffect;
    private Bat bat;
    private Rigidbody batRB;
      enum currentAttack
    {
        attack1,
        attack2,
        attack3,
        noAttack
    }
       currentAttack cAttack;
    public float AttackForce;
     private bool isCharging;
    public LayerMask LMask;
    private bool isCoolingOff;
    private bool canAttack = true;
    [Range(0.1f, 10f)]
    public float attackCoolOff = 1f;
    WaitForSeconds attackCoolOffDelay;
    private void Start()
    {
        attackCoolOffDelay = new WaitForSeconds(attackCoolOff);
        bat = GetComponentInChildren<Bat>();
        batRB = bat.GetComponent<Rigidbody>();
        bat.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isCoolingOff)
        {
            if (cAttack != currentAttack.noAttack)
            {
                Collider[] soroundingObjs = Physics.OverlapSphere(hand.position, sphereRadius, LMask);
                if (soroundingObjs.Length > 0)
                {
                    foreach (Collider col in soroundingObjs)
                    {
                        if (col.gameObject.GetComponent<Enemy>() != null)
                        {
                            col.gameObject.GetComponent<Enemy>().damage(5, true);
                            StartCoroutine(Cooloff());
                        }
                    }
                }

            }


        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawSphere(hand.position+sphereOffset, sphereRadius);

    }


  
    public void SetIncombo(int i)
    {

        GetComponentInParent<Rigidbody>().AddForce(transform.forward * AttackForce);

        switch (i)
        {
            case 0:
                batRB.isKinematic = false;
                batEffect.SetActive(true);
                cAttack = currentAttack.attack1;
                isCombing = true;
                sphereRadius = 5f;
            break;
            case 1:
                if(cAttack== currentAttack.attack1)
                {
                    canAttack = false;
                    StartCoroutine(AttackCooloff());
                    batRB.isKinematic = true;
                    isCombing = false;
                    sphereRadius = 0f;
                    player.Instance.playerAnimationManager.Player2Anim.SetBool("inAttack", false);
                    player.Instance.playerMovementManager.canmove = true;
                    batEffect.SetActive(false);


                }
                break;
            case 2:
                batRB.isKinematic = false;
                GetComponentInParent<Rigidbody>().AddForce(transform.forward * AttackForce*2);

                cAttack = currentAttack.attack2;
                batEffect.SetActive(true);

                break;
            case 3:
                batRB.isKinematic = true;
                if (cAttack == currentAttack.attack2)
                {
                    canAttack = false;
                    StartCoroutine(AttackCooloff());
                    isCombing = false;
                    sphereRadius = 0f;
                    player.Instance.playerAnimationManager.Player2Anim.SetBool("inAttack", false);
                    player.Instance.playerMovementManager.canmove = true;
                    batEffect.SetActive(false);
                }

                break;
            case 4:
                canAttack = false;
                StartCoroutine(AttackCooloff());
                batRB.isKinematic = false;
                GetComponentInParent<Rigidbody>().AddForce(transform.forward * AttackForce*3);

                cAttack = currentAttack.attack3;
                batEffect.SetActive(true);

                break;
            case 5:
                canAttack = false;
                StartCoroutine(AttackCooloff());
                batRB.isKinematic = true;
                isCombing = false;
                    sphereRadius = 0f;
                cAttack = currentAttack.noAttack;
                player.Instance.playerAnimationManager.Player2Anim.SetBool("inAttack", false);
                player.Instance.playerMovementManager.canmove = true;
                batEffect.SetActive(false);
              
                StartCoroutine(Cooloff());
                break;

        }
        //player.Instance.playerAnimationManager.player2Anim.SetBool("inCombo", iscombing);


    }
    

    public void AddAttackForce(float multiplier=1f)
    {
        player.Instance.playerMovementManager.ignoreMovement = true;

        GetComponentInParent<Rigidbody>().AddForce(transform.forward * AttackForce*multiplier);
        Debug.Log("add force");
    }
    public void StopAcceleration()
    {
        player.Instance.playerMovementManager.ignoreMovement = false;

 
    }
    public void BaseAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            player.Instance.playerMovementManager.canmove = false;

            player.Instance.playerAnimationManager.Player2Anim.SetTrigger("attack");
            player.Instance.playerAnimationManager.Player2Anim.SetBool("inAttack", true);
        }

    }

    public void StartHeavyAttack()
    {
        player.Instance.playerAnimationManager.Player2Anim.SetBool("superAttack", true);
        player.Instance.playerAnimationManager.Player2Anim.SetBool("inAttack", true);

        isCharging = true;
    }
    public void ReleaseHeavyAttack()
    {
        isCharging = false;
        player.Instance.playerAnimationManager.Player2Anim.SetBool("superAttack", false);
        StartCoroutine(Cooloff());
    }
   

    public IEnumerator AttackCooloff()
    {
        cAttack = currentAttack.noAttack;
        yield return attackCoolOffDelay;
        canAttack = true;
        player.Instance.playerMovementManager.ignoreMovement = false;
    }
    IEnumerator Cooloff()
    {
        isCoolingOff = true;
        yield return new WaitForSeconds(1f);
        isCoolingOff = false;
    }
}
