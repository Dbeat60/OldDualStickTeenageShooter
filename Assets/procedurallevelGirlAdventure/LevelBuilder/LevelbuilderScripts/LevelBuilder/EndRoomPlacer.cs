using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoomPlacer : MonoBehaviour {
    
    public roompg endRoomPrefab;
    [HideInInspector]
    public endRoom endRoom;

    public void PlaceEndRoom()
    {
        Debug.Log("place end room");
        endRoom = Instantiate(endRoomPrefab) as endRoom;
        endRoom.transform.parent = this.transform;

        List<doorpg> allAvailableDoorways = new List<doorpg>(LevelBuilder.Instance.availableDoorways);
        doorpg doorway = endRoom.doors[0];
        bool roomplaced = false;
        foreach (doorpg availableDoor in allAvailableDoorways)
        {
            roompg room = (roompg)endRoom;
            LevelBuilder.Instance.roomConnecter.PositionRoomAtDoorway(ref room, doorway, availableDoor);

            if (LevelBuilder.Instance.roomOverlapChecker.checkRoomOverlap(endRoom))
            {
                continue;
            }
            roomplaced = true;
            doorway.gameObject.SetActive(false);
            LevelBuilder.Instance.availableDoorways.Remove(doorway);

            availableDoor.gameObject.SetActive(false);
            LevelBuilder.Instance.availableDoorways.Remove(availableDoor);
            for (int i = 0; i < transform.childCount; i++)
            {
                LevelBuilder.Instance.PLNAV.navmeshes.Add(transform.GetChild(i).GetComponent<UnityEngine.AI.NavMeshSurface>());

            }
            LevelBuilder.Instance.PLNAV.recalculateNavmesh();

            break;

        }
        if (!roomplaced)
        {
            Debug.LogWarning("End room not placed...RESETING");

            LevelBuilder.Instance.ResetLevelGenerator();
        }
    }
}
