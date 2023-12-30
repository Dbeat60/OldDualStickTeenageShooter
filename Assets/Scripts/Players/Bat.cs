using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField]
    private GameObject hitParticle;
    private GameObject hitParticleInstance;
    [SerializeField]
    private float hitParticleDuration = 0.1f;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        hitParticleInstance = Instantiate(hitParticle, transform.position, Quaternion.identity);
        hitParticleInstance.SetActive(false);
    }
    IEnumerator deactivateHitParticle()
    {
        yield return new WaitForSeconds(hitParticleDuration);
        hitParticleInstance.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {        
           hitParticleInstance.transform.position =  collision.contacts[0].point;
           hitParticleInstance.SetActive(true);
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.gameObject.GetComponent<Enemy>().damage(20, false);
        }


    }
}
