using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 public class roompg : MonoBehaviour {

   public doorpg[] doors;
   // public List<Collider> colliders = new List<Collider>();
    public Bounds bound;
    MeshCollider MC;
    public MeshFilter[] meshFilters;
    Vector3 originalPos,originalScale;
    Quaternion originalRot;
 public   bool hassubmeshes=true;
   public MeshCollider childmesh;
    void combineMeshes()
    {
        if (hassubmeshes)
        {
            originalPos = transform.position;
            originalScale = transform.localScale;
            originalRot = transform.rotation;
            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            MC = GetComponent<MeshCollider>();
            meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];
            int i = 0;
            while (i < meshFilters.Length - 1)
            {
                combine[i].mesh = meshFilters[i + 1].sharedMesh;
                combine[i].transform = meshFilters[i + 1].transform.localToWorldMatrix;
                // meshFilters[i].gameObject.SetActive( false);
                i++;
            }
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);
            MC.sharedMesh = transform.GetComponent<MeshFilter>().mesh;
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.position = originalPos;
            transform.localScale = originalScale;
            transform.rotation = originalRot;
        }
        else
        {
            MC = childmesh;
             
        }

    }
    public bool iscolliding;
    
    [SerializeField]
    Collider[] colliders;
    private void OnDrawGizmos()
    {
        if (LevelBuilder.Instance != null && LevelBuilder.Instance.showdebugcolliders)
        {
            bound = getBounds;
            bound.Expand(LevelBuilder.Instance.roomOverlapChecker.boundoffset);
            colliders = Physics.OverlapBox(bound.center, bound.size / 2, transform.rotation, LevelBuilder.Instance.roomOverlapChecker.roomLayerMask);

            if (colliders.Length > 0)
            {
                Gizmos.color = Color.red;
                iscolliding = true;
            }
            else
            {
                Gizmos.color = Color.green;
                iscolliding = false;
            }
            Gizmos.DrawCube(bound.center, bound.size);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
         iscolliding = true;
        Destroy(gameObject);
    }
    public Bounds getBounds 
    {
        get {
            combineMeshes();
            bound = MC.bounds;
            return bound;
        }
    }

	 
	 
	 
}
