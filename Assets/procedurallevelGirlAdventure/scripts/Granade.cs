using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : ProjectileAC
{
    // Use this for initialization
    Rigidbody rb;
    public float timealive = 3f;
    public float explosionpower = 10f;
    public float explosionRadius = 10f;
    public GameObject Explosionprefab;
    public GameObject explosionDecal;

    void Start ()
    {
        Destroy(gameObject, timealive);
        rb = GetComponent<Rigidbody>();
	}
    
    private void OnDisable()
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

        foreach (Collider c in hitColliders)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null && r != rb)
                r.AddExplosionForce(explosionpower, transform.position, explosionRadius);


        }
        Instantiate(Explosionprefab, transform.position, Quaternion.identity);
        Instantiate(explosionDecal, transform.position, explosionDecal.transform.rotation);
        Destroy(gameObject);
    }

    
	// Update is called once per frame
	void Update ()
    {
		 
	}
}
