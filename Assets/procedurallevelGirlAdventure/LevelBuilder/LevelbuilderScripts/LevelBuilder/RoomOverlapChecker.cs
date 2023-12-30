using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOverlapChecker : MonoBehaviour {

    public LayerMask roomLayerMask;
    public float boundoffset = 0.4f;     //check for posible overlaps


    private void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
    }
    public bool checkRoomOverlap(roompg room)
    {
        //get room bounds
        Bounds bounds = room.getBounds;
        //reduce bounds to avoid minimum ovelaps
        bounds.Expand(boundoffset);
        //room.colliders.Clear();
        //check for posible collisions
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
        if (colliders.Length > 0)
        {
            room.GetComponentInChildren<Renderer>().material.color = Color.red;

            //print(colliders.Length);
            foreach (Collider c in colliders)
            {
                //room.colliders.Add(c);
                if (c.transform.parent.gameObject.Equals(room.gameObject))
                    continue;
                else
                {
                    //print(room.name+ " overlaps with: "+c.transform.parent.parent.name + colliders.Length);
                    Debug.LogWarning("Ovelap detected...Retrying");
                    return true;
                }

            }
            {

            }
        }
        else
            room.GetComponentInChildren<Renderer>().material.color = Color.white;

        return false;

    }
}
