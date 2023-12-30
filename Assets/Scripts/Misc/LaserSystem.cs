using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[ExecuteInEditMode]
public class LaserSystem : MonoBehaviour {

    LineRenderer LR;
    [SerializeField]
    private GameObject midpointObject;
    Transform startpos, endpos;
   public List<Transform> LaserPositions = new List<Transform>();
    [SerializeField]
    bool debug;
    [SerializeField]
    bool updatealways;
    public Color LaserColor;
    [SerializeField]
    private float laserLenght=0;
    public bool isLaserHazardous=true;
    public void RemoveLaserPoint(Transform objToRemove)
    {
        if (LaserPositions.Contains(objToRemove))
        {
            LaserPositions.Remove(objToRemove);
            //RecalculateLaser();
        } 
    }
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            if (startpos != null && endpos != null)
            {
                Gizmos.DrawSphere(startpos.position, 1f);
                Gizmos.DrawSphere(endpos.position, 1f);
                foreach (Transform t in LaserPositions)
                    Gizmos.DrawSphere(t.position, 1f);
            }
        }
    }
    [Button("Recalculate Laser")]
    public void RecalculateLaser()
    {
        LaserPositions.Clear();
        Start();
    }
    // Use this for initialization
    void Start()
    {
        LaserPositions.Clear();

        LR = GetComponent<LineRenderer>();
        LR.startColor = LaserColor;
        LR.endColor= LaserColor;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "origin")
                startpos = transform.GetChild(i);
            else if (transform.GetChild(i).name == "End")
                endpos = transform.GetChild(i);
            else
                LaserPositions.Add(transform.GetChild(i));

        }
        if (startpos != null || endpos != null)
        {
            LaserPositions.Add(endpos);
            LaserPositions.Insert(0, startpos);
        }
        else if (startpos == null || endpos == null)
        {
            if (LaserPositions.Count >= 2)
            {
                if (startpos == null)
                {
                    startpos = LaserPositions[0];
                    LaserPositions.RemoveAt(0);
                }
                if (endpos == null)
                {
                    endpos = LaserPositions[LaserPositions.Count - 1];
                    LaserPositions.RemoveAt(LaserPositions.Count - 1);
                }
            }
            else
            {
                print("Bad laser System");
                Destroy(gameObject);
            }
        }

        calculateLaser();
    }

    private void calculateLaser()
    {
        LR.positionCount = LaserPositions.Count + 2;
        LR.SetPosition(0, startpos.position);
        for (int i = 0; i < LaserPositions.Count; i++)
        {
            if (!LaserPositions[i])
                RecalculateLaser();
            LR.SetPosition(i + 1, LaserPositions[i].position);
        }
        LR.SetPosition(LR.positionCount - 1, endpos.position);
        if (startpos && endpos && LaserPositions.Count >= 2)
        {
            startpos.LookAt(LaserPositions[1]);
            startpos.Rotate(Vector3.up, -90);
            endpos.LookAt(LaserPositions[LaserPositions.Count - 2]);
            endpos.Rotate(Vector3.up, 90);
        }
    }
    [Button("Add midPoint")]
    public void AddMidPoint()
    {
        if (startpos && endpos)
        {
            LaserModule lm = Instantiate(midpointObject, (LaserPositions[LaserPositions.Count - 2].position + LaserPositions[LaserPositions.Count-1].position) * 0.5f, Quaternion.identity).GetComponent<LaserModule>();
            lm.transform.SetParent(transform);
            lm.laserParent = this;
            lm.isMidPoint = true;
            lm.Setup();
            RecalculateLaser();
        }
    }
    // Update is called once per frame
    void Update ()
    {
        if (updatealways)
        {
            calculateLaser();
            if (isLaserHazardous)
            {
                CheckLaserCollisions();
            }
        }
	}
    IEnumerator cooloff()
    {
        isLaserHazardous = false;
        yield return new WaitForSeconds(3.5f);
        isLaserHazardous = true;
            
    }
    void CheckLaserCollisions()
    {
        for (int i = 1; i < LaserPositions.Count; i++)
        {
            laserLenght = Vector3.Distance(startpos.position, LaserPositions[i].position);
 
            Ray ray = new Ray(LaserPositions[i - 1].position, (LaserPositions[i].position - LaserPositions[i - 1].position));
            RaycastHit hito;
            if (Physics.Raycast(ray, out hito, laserLenght))
            {
                //                Debug.Log(hito.transform.name);
                if (hito.transform.tag == "Player")
                {
                    if (!player.Instance.isJumping)
                    {
                        player.Instance.playerHealthManager.callUpdateHealth(80f);
                        StartCoroutine(cooloff());


                    }

                    Debug.DrawLine(LaserPositions[i - 1].position, LaserPositions[i].position, Color.red);
                }
                else
                    Debug.DrawLine(LaserPositions[i - 1].position, LaserPositions[i].position, Color.yellow);


            }
            else
            {
                Debug.DrawLine(LaserPositions[i - 1].position, LaserPositions[i].position, Color.white);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (player.Instance.isJumping)
            {
                
            }
            else
                player.Instance.playerHealthManager.callUpdateHealth(80f);
        }
    }
}
