using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogTrigger3D : dialogTrigger2D {

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!loop && !firsttime)
            {
                Destroy(gameObject);
            }
            else
            {
                dialogSystemHolder.SetActive(true);
                dialogSystemHolder.GetComponent<dialogSystem>().StartDialog(npcNameS, dialogLS);
                firsttime = false;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!loop && !firsttime)
            {
                Destroy(gameObject);
            }
            else
            {
                 dialogSystemHolder.GetComponent<dialogSystem>().resetDialogSystem();
             }
        }

    }
    // Use this for initialization
    void Start ()
    {
        dialogSystemHolder = GameObject.Find("CanvasDS").transform.GetChild(0).gameObject ;


    }

    // Update is called once per frame
    void Update () {
		
	}
}
