using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class gasBarrel : NPCHealth {

    Rigidbody rb;
    public float explosionpower = 10f;
    public float explosionRadius = 10f;
    public GameObject Explosionprefab;
    public GameObject explosionDecal;

    void Start()
    {
        SetupHealth();
         rb = GetComponent<Rigidbody>();
    }
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

        
        Instantiate(Explosionprefab, transform.position, Quaternion.identity);
        Instantiate(explosionDecal, transform.position, explosionDecal.transform.rotation);
        foreach (Collider c in hitColliders)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null && r != rb)
                r.AddExplosionForce(explosionpower, transform.position, explosionRadius);


        }
     }
    private void OnDestroy()
    {
      //  Debug.Log(health);

        if (health<=0)
        Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "bullet")
        {
            AffectHealth(10);
             
        }
    }
    [Button("Test Damage")]
    public void Damage()
    {
        Debug.Log(health);
        AffectHealth(100);
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Damage();
	}
}
