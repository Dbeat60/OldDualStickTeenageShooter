/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Old : MonoBehaviour {

    protected int health;
    public GameObject DeadEffect;
    [SerializeField]
    private GameObject damageEffect;
    public EnemySpawner spawner;
    public Transform damageParticlePosition;
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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public int getHealth()
    {
        return health;
    }
    public void Dead()
    {
        if (DeadEffect != null)
        {
            Instantiate(DeadEffect, (damageParticlePosition ? damageParticlePosition.position : transform.position), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
*/