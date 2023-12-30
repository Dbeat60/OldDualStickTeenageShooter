using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
public class EnemySpawner : MonoBehaviour
{

    // Public members

    public UnityEvent spawnPointFinished;

    // Private members

    [SerializeField]
    private int numEnemies = 5;
    [SerializeField]
    private TextMesh enemyCounterMesh;
    [SerializeField]
    private float timeBetweenEnemies = 3.0f;
    [SerializeField]
    private int spawnedEnemiesPerCycle = 1;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private bool startAwake = false;
    [SerializeField]
    private int activations = 1;
    [SerializeField]
    private int activationsToSkip = 0;

    private bool spawning;
    public int remainingEnemies;

    public RoomManager roomMG;
    [SerializeField]
    [MinMaxSlider(-150, 150)]
    private Vector2 XRange;
    [SerializeField]
    [MinMaxSlider(-150, 150)]
    private Vector2 ZRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, .3f);
        Gizmos.DrawCube(new Vector3(XRange.x, transform.position.y, ZRange.x),Vector3.one*5f);
        Gizmos.DrawCube(new Vector3(XRange.x, transform.position.y, ZRange.y), Vector3.one*5f);
        Gizmos.DrawCube(new Vector3(XRange.y, transform.position.y, ZRange.x), Vector3.one*5f);
        Gizmos.DrawCube(new Vector3(XRange.y, transform.position.y, ZRange.y), Vector3.one*5f);
    }
    public Vector3 getNewEmptyTarget()
    {
 
        return new Vector3(UnityEngine.Random.Range(XRange.x, XRange.y), transform.position.y, UnityEngine.Random.Range(ZRange.x, ZRange.y));
    }
    // Initialization
    public void Setup()
    {
        enemyCounterMesh = GetComponentInChildren<TextMesh>();
        if (enemyCounterMesh != null)
        {
            enemyCounterMesh.text = numEnemies.ToString();
        }

        spawning = false;
        remainingEnemies = numEnemies;

      
            StartSpawning();
         
    }
    private void Start()
    {
        if(startAwake)
        Setup();
    }

    // Public methods

    public void StartSpawning()
    {
        if (!spawning)
        {
            if (activationsToSkip > 0)
            {
                activationsToSkip--;
            }
            else if (activations > 0)
            {
                activations--;
                remainingEnemies = numEnemies;
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    public void StopSpawning()
    {
        if (spawning)
        {
            StopCoroutine(SpawnEnemies());
        }
    }

    // Private methods

    private IEnumerator SpawnEnemies()
    {
        spawning = true;

        WaitForSeconds wait = new WaitForSeconds(timeBetweenEnemies);
        
        while (remainingEnemies > 0)
        {
            SpawnEnemy();
            yield return wait;
        }

        spawnPointFinished.Invoke();
        spawning = false;
        roomMG.CheckEnemiesRemaining();
    }

    private void SpawnEnemy()
    {
        Enemy c= Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform).GetComponent<Enemy>();
        c.spawner = this;
        remainingEnemies--;
        Debug.Log("ad");
        Update3DUI();
    }

    private void Update3DUI()
    {
        if (enemyCounterMesh != null)
        {
            enemyCounterMesh.text = remainingEnemies.ToString();
        }
    }

}