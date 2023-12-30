using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : MonoBehaviour {

    public roompg startRoomprefab;
    public List<roompg> roomsPrefabs = new List<roompg>();


    public void PlaceRoom(roompg croom)
    {
        //create new room
        Debug.Log("place random room from List");
        roompg currentRoom = croom;


        currentRoom.transform.parent = this.transform;
        //create doorlists
        List<doorpg> allAvailableDoorways = new List<doorpg>(LevelBuilder.Instance.availableDoorways);
        List<doorpg> currentRoomDoorways = new List<doorpg>();

        LevelBuilder.Instance.AddDoorwaystoList(currentRoom, ref currentRoomDoorways);
        //add current room doors
        LevelBuilder.Instance.AddDoorwaystoList(currentRoom, ref LevelBuilder.Instance.availableDoorways);
        bool roomPlaced = false;

        //try all available doorways
        foreach (doorpg availableDoorway in allAvailableDoorways)
        {

            //try all available doorways in the room
            foreach (doorpg currentDoorway in currentRoomDoorways)
            {
                //position room
                LevelBuilder.Instance.roomConnecter.PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
                //check for posible overlaps

                if (LevelBuilder.Instance.roomOverlapChecker.checkRoomOverlap(currentRoom))
                {
                    continue;
                }
                roomPlaced = true;
                //add room to list
                LevelBuilder.Instance.placedRooms.Add(currentRoom);

                //remove current room ocupied doorways(change wall mesh for door mesh)
                currentDoorway.gameObject.SetActive(false);
                LevelBuilder.Instance.availableDoorways.Remove(currentDoorway);

                //remove connected room ocupied doorways(change wall mesh for door mesh)
               // availableDoorway.gameObject.SetActive(false);
                LevelBuilder.Instance.availableDoorways.Remove(availableDoorway);

                break;
            }
            if (roomPlaced)
            {
                break;
            }

        }
        //level generation failed,abort and restart
        if (!roomPlaced)
        {
            Debug.LogError("room not placed...RESETING");

            Destroy(currentRoom.gameObject);
            // ResetLevelGenerator();

        }

    }
    public void PlaceRoom()
    {
        //create new room
        Debug.Log("place random room from List");
        roompg currentRoom = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Count)]) as roompg;


        currentRoom.transform.parent = this.transform;
        //create doorlists
        List<doorpg> allAvailableDoorways = new List<doorpg>(LevelBuilder.Instance.availableDoorways);
        List<doorpg> currentRoomDoorways = new List<doorpg>();

        LevelBuilder.Instance.AddDoorwaystoList(currentRoom, ref currentRoomDoorways);
        //add current room doors
        LevelBuilder.Instance.AddDoorwaystoList(currentRoom, ref LevelBuilder.Instance.availableDoorways);
        bool roomPlaced = false;

        //try all available doorways
        foreach (doorpg availableDoorway in allAvailableDoorways)
        {

            //try all available doorways in the room
            foreach (doorpg currentDoorway in currentRoomDoorways)
            {
                //position room
                LevelBuilder.Instance.roomConnecter.PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
                //check for posible overlaps

                if (LevelBuilder.Instance.roomOverlapChecker.checkRoomOverlap(currentRoom))
                {
                    continue;
                }
                roomPlaced = true;
                //add room to list
                LevelBuilder.Instance.placedRooms.Add(currentRoom);

                //remove current room ocupied doorways(change wall mesh for door mesh)
                currentDoorway.gameObject.SetActive(false);
                LevelBuilder.Instance.availableDoorways.Remove(currentDoorway);

                //remove connected room ocupied doorways(change wall mesh for door mesh)
                availableDoorway.gameObject.SetActive(false);
                LevelBuilder.Instance.availableDoorways.Remove(availableDoorway);

                break;
            }
            if (roomPlaced)
            {
                break;
            }

        }
        //level generation failed,abort and restart
        if (!roomPlaced)
        {
            Debug.LogWarning("room not placed...RESETING");

            Destroy(currentRoom.gameObject);

            LevelBuilder.Instance.ResetLevelGenerator();

        }

    }

  

}
