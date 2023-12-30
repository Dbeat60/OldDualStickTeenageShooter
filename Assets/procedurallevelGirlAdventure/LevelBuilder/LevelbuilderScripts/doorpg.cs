using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorpg : MonoBehaviour {

    private void OnDrawGizmos()
    {
        Ray r = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(r);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
