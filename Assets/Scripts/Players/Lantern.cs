using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	
    [SerializeField] private LayerMask lightLayerMask;
    [SerializeField] private float distance=100;
    private cacodemon cDemon;
    // Update is called once per frame
    void Update()
    {

        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit rhit;

        Debug.DrawRay(transform.position, transform.forward * distance);
        if (Physics.Raycast(r, out rhit, distance, lightLayerMask))
        {
            cDemon = rhit.transform.GetComponent<cacodemon>();
            /* if (cDemon!=null)
             {
                 if(!cDemon.isIluminated)
                 cDemon.changemat();
             }*/

        }

        else if (cDemon != null)
        {
            // cDemon.changemat();
            // cDemon = null;
        }

    }
}
