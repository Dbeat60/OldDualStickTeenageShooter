using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
   public RoomSettings settings;
   public List<EnemySpawner> enemySpawners;
    public List<ActivationSystem> doors;
    bool Activated;
    public bool showGizmo;
    BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].roomMG = this;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            if(col == null)
            {
                col = GetComponent<BoxCollider>();
            }
            Gizmos.color = new Color(1f, 1f, 0f,0.42f);
            Gizmos.DrawCube(transform.position + col.center, col.size);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Activated)
        {
            if (other.tag == "Player")
            {
                Activated = true;
                StartSetup();
            }
        }
    }
    public void ReleaseDoor()
    {
        print("hodoer");
        for (int i = 0; i < doors.Count; i++)
        {
            if(doors[i].IsLocked)
            {
                doors[i].Unlock();
            }
        }
    }
    public bool CheckEnemiesRemaining()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            if (enemySpawners[i].remainingEnemies>0)
            {

                return true;
            }
        }
        ReleaseDoor();
        return false;
    }
    void StartSetup()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].Setup();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
