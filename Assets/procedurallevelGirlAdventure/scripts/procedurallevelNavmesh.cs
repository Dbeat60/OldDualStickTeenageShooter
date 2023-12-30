using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class procedurallevelNavmesh : MonoBehaviour {

    public List<NavMeshSurface> navmeshes = new List<NavMeshSurface>();
	// Use this for initialization
	void Start ()
    {
		
	}
	
 public   void recalculateNavmesh()
    {
        foreach(NavMeshSurface NS in navmeshes)
        {
            NS.BuildNavMesh();
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J))
            recalculateNavmesh();
		
	}
}
