using UnityEngine;

public static class Vector3Extensions  {

	 public static bool goingBackwards(this Vector3 movementvec,Vector3 rotvec )
    {
        return (Vector3.Dot(movementvec, rotvec) < 0);

    }
} 
