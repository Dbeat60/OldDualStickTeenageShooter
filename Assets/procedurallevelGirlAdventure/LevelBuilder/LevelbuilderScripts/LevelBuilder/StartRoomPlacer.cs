using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomPlacer : MonoBehaviour {

    public roompg startRoomprefab;
    [HideInInspector]
    public startRoom startRoom;

   public void PlaceStartRoom()
    {
        Debug.Log("Place Start Room");
        startRoom = Instantiate(startRoomprefab) as startRoom;
        startRoom.transform.parent = transform;
        LevelBuilder.Instance.AddDoorwaystoList(startRoom, ref LevelBuilder.Instance.availableDoorways);
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;

    }
}
