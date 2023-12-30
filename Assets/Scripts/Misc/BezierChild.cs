using UnityEngine;

public class BezierChild : LaserDrawer
{

    // Private members

    private bool isValid;

    // Initialization

    private void Start()
    {
        isValid = transform.parent.tag == "Laser";
    }

    // Main loop

    private void Update()
    {
        if (isValid)
        {
            Debug.Log("sd");
            p3p = p2p = p1p = p0p = transform.parent.GetComponent<LaserDrawer>().p3p;
        }
    }

}
