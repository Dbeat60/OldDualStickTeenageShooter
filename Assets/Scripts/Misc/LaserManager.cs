using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {

    public GameObject laserPrefab;
    
    public float laserRange = 1f;
    public float maxlaserRange = 3f;
   
    SphereCollider spCollider;
    public List<GameObject> activeLasers = new List<GameObject>();
    [SerializeField]
    shakeTransform shakeManager;
    [SerializeField]
    CameraShakeData shakeData;
    public List<GameObject> enemies = new List<GameObject>();
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, laserRange);
    }
    private void Awake()
    {
        spCollider = GetComponent<SphereCollider>();
    }
    // Use this for initialization
    
    void updateLasers()
    {
        for (int i = 0; i < activeLasers.Count; i++)
        {
            GameObject g = activeLasers[i];
            if (i == 0)
                g.GetComponent<LaserDrawer>().SetupLaser(transform, enemies[0].transform);
            else
                g.GetComponent<LaserDrawer>().SetupLaser(enemies[enemies.Count - 2].transform, enemies[enemies.Count - 1].transform);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(other.GetComponent<Enemy>().getHealth()<=0)
            {
                print(other.name + "out");
                enemies.Remove(other.gameObject);
                GameObject al= other.GetComponentInChildren<LaserDrawer>().gameObject;
                if(al!=null)
                activeLasers.Remove(al);
                other.GetComponent<Enemy>().Dead();                
                updateLasers();
                   

                
            }
            else
            other.GetComponent<Enemy>().damage(1);
            
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!enemies.Contains(other.gameObject))
            {
                print(other.name + "in");
                enemies.Add(other.gameObject);
                if (shakeData != null && shakeManager != null)
                {
                    shakeManager.AddShakeEvent(shakeData);
                }
                GameObject g = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                activeLasers.Add(g);

                if (enemies.Count == 1)
                    g.GetComponent<LaserDrawer>().SetupLaser(transform, enemies[0].transform);
                else
                    g.GetComponent<LaserDrawer>().SetupLaser(enemies[enemies.Count-2].transform, enemies[enemies.Count-1].transform);
                g.transform.SetParent(enemies[enemies.Count - 1].transform);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print(other.name + "out");
            enemies.Remove(other.gameObject);
            GameObject g = activeLasers[0];
            activeLasers.RemoveAt(0);
            Destroy(g);
        }
    }
   
    void Update ()
    {
      

    }

    public void resetLaser()
    {
        print("nopew");
        tdelta = 0f;
        laserRange = 0;
        spCollider.radius = laserRange;
        laserRange = Mathf.Lerp(laserRange, 0, Time.deltaTime);

        spCollider.radius = laserRange;
    }
    float tdelta = 0f;
    public void shootLaser()
    {
        print("pew");
        laserRange = Mathf.Lerp(laserRange, maxlaserRange, tdelta);
        tdelta += Time.deltaTime;

        spCollider.radius = laserRange;
        for (int i = 0; i < activeLasers.Count; i++)
        {
            if(activeLasers[i]!=null)
            activeLasers[i].GetComponent<LaserDrawer>().ShootLaser();
        }
    }
}
