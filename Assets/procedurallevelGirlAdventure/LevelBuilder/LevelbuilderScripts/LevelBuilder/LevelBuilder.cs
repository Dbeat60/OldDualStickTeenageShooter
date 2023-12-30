using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public static LevelBuilder _instance;   
   
    public Vector2 iterationRange = new Vector2(3, 10);
    public List<doorpg> availableDoorways = new List<doorpg>();
    public List<roompg> placedRooms = new List<roompg>();
    public List<roompg> colidingRooms = new List<roompg>();
    public procedurallevelNavmesh PLNAV;


    bool paused;
    public GameObject LoadingScreen;
    public bool showdebugcolliders;

    public bool canbepaused;


    #region Helper Classes
    [HideInInspector]
     public RoomOverlapChecker roomOverlapChecker;
     public RoomConnecter roomConnecter;
     RoomPlacer roomPlacer;
    StartRoomPlacer startRoomPlacer;
    EndRoomPlacer endRoomPlacer;
    #endregion

    private void Awake()
    {
        _instance = this;
    }
    public static LevelBuilder Instance
    {
        get { return _instance; }
    }
    void Start()
    {
        PLNAV = GetComponent<procedurallevelNavmesh>();

        StartCoroutine("GenerateLevel");
        roomOverlapChecker = GetComponent<RoomOverlapChecker>();
        roomConnecter = GetComponent<RoomConnecter>();
        roomPlacer = GetComponent<RoomPlacer>();
        startRoomPlacer = GetComponent<StartRoomPlacer>();
        endRoomPlacer = GetComponent<EndRoomPlacer>();



    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;
        startRoomPlacer.PlaceStartRoom();
        yield return interval;
        int iteration = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for (int i = 0; i < iteration; i++)
        {
            while (paused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            roomPlacer.PlaceRoom();
            if (canbepaused)
                paused = true;
            yield return interval;

        }


        endRoomPlacer.PlaceEndRoom();
        yield return interval;
        Debug.Log("Generation finished");
        LoadingScreen.SetActive(false);

        // yield return new WaitForSeconds(3);
        // ResetLevelGenerator();
    }

    void trytofix()
    {
        for (int i = 0; i < placedRooms.Count; i++)
        {
            if (placedRooms[i].iscolliding)
            {
                Destroy(placedRooms[i].gameObject, 0.2f);
                placedRooms.Remove(placedRooms[i]);

                //colidingRooms.Add(placedRooms[i]);
                //placedRooms.Remove(placedRooms[i]);

            }

        }
    }
    
   public void  AddDoorwaystoList(roompg room, ref List<doorpg> doorsList)
    {
        foreach(doorpg d in room.doors)
        {
            int r = Random.Range(0, doorsList.Count);
            doorsList.Insert(r, d);
        }
    }

   
   
    //position room
   
    
              
    public void ResetLevelGenerator()
    {
        Debug.LogWarning("reset level generator");
        if(startRoomPlacer.startRoom)
        {
            Destroy(startRoomPlacer.startRoom.gameObject);
        }
        if(endRoomPlacer.endRoom)
        {
            Destroy(endRoomPlacer.endRoom.gameObject);
        }
        foreach(roompg r in placedRooms)
        {
            Destroy(r.gameObject);
        }
        availableDoorways.Clear();
        placedRooms.Clear();
        StopCoroutine("GenerateLevel");
        StartCoroutine("GenerateLevel");
    }


	// Use this for initialization
	

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetLevelGenerator();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            trytofix();
        }
        if (Input.GetKeyDown(KeyCode.P))
            paused = !paused;
	}
}
