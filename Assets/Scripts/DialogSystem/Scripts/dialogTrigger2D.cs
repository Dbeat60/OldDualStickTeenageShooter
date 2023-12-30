using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogTrigger2D : MonoBehaviour {

    [SerializeField]
    protected string npcNameS;     
    [SerializeField]
    protected  GameObject dialogSystemHolder;
    [SerializeField][TextArea(0,5)]
    protected List<string> dialogLS;
    public bool loop=true;
    protected bool firsttime=true;
    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerExit2D(Collider2D other)
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
                firsttime = false;
            }
        }

    }
    [SerializeField]
    bool useFile;
    // Use this for initialization
    void Start ()
    {
        dialogSystemHolder = GameObject.Find("CanvasDS").transform.GetChild(0).gameObject;
        if(useFile)
        {
            dialogLS = dialogSystemHolder.GetComponent<XmlManager>().ReadData(npcNameS);
             
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
