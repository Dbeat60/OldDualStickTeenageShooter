using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnecter : MonoBehaviour {

    public void PositionRoomAtDoorway(ref roompg room, doorpg roomDoorway, doorpg targetDoorWay)
    {
        //resets room transform
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;
        //rotate room to match the other door
        Vector3 targetDoorwayEuler = targetDoorWay.transform.eulerAngles;
        Vector3 roomDoorWayEuler = roomDoorway.transform.eulerAngles;
        //gets the smallest angle difference between 2 angles
        float deltaAngle = Mathf.DeltaAngle(roomDoorWayEuler.y, targetDoorwayEuler.y);
        //creates a new quaternion with the previous door rotation
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        //inverse the rotation to make the correct connection
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0f, 180f, 0f);
        //setsup the position
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;

        room.transform.position = targetDoorWay.transform.position - roomPositionOffset;
    }
}
