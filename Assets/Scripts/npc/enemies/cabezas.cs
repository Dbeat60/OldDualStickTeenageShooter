using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cabezas : Enemy
{
    bool canAttack = true;
    [SerializeField] float ExplosionDistance = 4f;
    [SerializeField]  float AttackSpeedMultiplier = 2.5f;
    // Start is called before the first frame update
    
    
    public override void Attack()
    {
        base.Attack();
        if (canAtack)
        {
            anim.SetTrigger("Attack");
            canAtack = false;
            speed *= AttackSpeedMultiplier;          
        }
        




    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(agent.destination, 3f);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // lookAtY();

        if (CurrentState== EnemyState.Attack && distance <= ExplosionDistance)
        {
            playerCamera.instance.shake();
            Dead();

        }
    }
}
