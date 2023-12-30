using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAC : MonoBehaviour
{
    public void shoot(Vector3 Force)
    {
        GetComponent<Rigidbody>().AddForce(Force);
    }
}
