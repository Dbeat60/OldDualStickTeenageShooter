using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : ProjectileAC
{
    private float lifetime = 2f;
   
     [SerializeField]
    shakeTransform shakeManager;
    [SerializeField]
    CameraShakeData shakeData;
    public GameObject spark;
    public float collisionRadius = 10f;
    public bool isPlayerbullet = true;
    IEnumerator bulletlifespan()
    {
        yield return new WaitForSeconds(lifetime);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
           PoolSystem.Instance.AddtoPool(gameObject,isPlayerbullet);

    }
    private void OnEnable()
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        shakeManager = GameObject.FindObjectOfType<shakeTransform>();
        StartCoroutine(bulletlifespan());
         

    }
    // Use this for initialization
    void Start ()
    {
		
	}
    //TODO DELETE
   //
    // Update is called once per frame
    void FixedUpdate ()
    {

        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit collision;
        if(Physics.SphereCast(r,1f, out collision, collisionRadius ))
        {
               Debug.Log("bullet col " + collision.transform.name);

            if (isPlayerbullet)
            {
                
                if (collision.transform.tag == "Enemy")
                {
                    collision.transform.gameObject.GetComponent<Enemy>().damage(10, false);
                   PoolSystem.Instance.AddtoPool(gameObject, true);

                }
                else if (collision.transform.GetComponent<Health>() != null && collision.transform.tag != "Player")
                {
                    spark.SetActive(true);
                     Instantiate(spark, transform.position, Quaternion.identity);

                    collision.transform.GetComponent<Health>().AffectHealth(10);
                    PoolSystem.Instance.AddtoPool(gameObject, true);

                }
                else
                {
                    spark.SetActive(true);

                    Instantiate(spark, transform.position, Quaternion.identity);

                    PoolSystem.Instance.AddtoPool(gameObject,true);
                }
            }
            else
            {
                if (collision.transform.tag == "Player")
                {
                    player.Instance.playerHealthManager.callUpdateHealth(10);
                    //collision.transform.gameObject.GetComponent<Enemy>().damage(10, false);
                    PoolSystem.Instance.AddtoPool(gameObject, false);

                }
                else
                {
                    spark.SetActive(true);

                    //Instantiate(spark, transform.position, Quaternion.identity);

                    PoolSystem.Instance.AddtoPool(gameObject, false);
                }
            }
        }
	}
}
