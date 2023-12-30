using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RaptorHealth : NPCHealth {

    private raptorAI rapAI;
    public float DeadDelay = 3f;
    // Use this for initialization
    void Start()
    {
        SetupHealth();
        rapAI = GetComponent<raptorAI>();
    }
    [Button("TestDamageHealth")]
    public void Damage()
    {
        rapAI.Anim.SetTrigger("damage");
        rapAI.AI.CoolOff();
        if (health - 10 > 0)
            AffectHealth(10);
        else
            StartCoroutine(delayedDead());
    }
    IEnumerator delayedDead()
    {
        rapAI.Anim.SetTrigger("dead");
        rapAI.AI.agent.isStopped = true;
        rapAI.AI.agent.enabled =false;

        yield return new WaitForSeconds(DeadDelay);
        AffectHealth(10);

    }
    
}
