using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {

    public Button[] btns;
    public int btnindex;
    public float waittime = 0.5f;
    public float currenttime = 0f;
    float deltaTime = 0.005f;
	// Use this for initialization
	void Start ()
    {
        btns[btnindex].GetComponent<Image>().color = Color.red;


    }

    void changebtn(int i)
    {

        btns[btnindex].GetComponent<Image>().color = Color.white;
        btnindex+= i;
        if (btnindex >= btns.Length)
            btnindex = 0;
        else if (btnindex < 0)
            btnindex = btns.Length - 1;
        btns[btnindex].GetComponent<Image>().color = Color.red;


    }
    // Update is called once per frame
    void Update ()
    {
        

         if(Input.GetAxis("Vertical")<-0.1f)
        {
        
            if (currenttime > waittime)
            {
                changebtn(1);
                currenttime = 0f;
            }
            else
            {
                currenttime += deltaTime;
                
            }
        }
        else if (Input.GetAxis("Vertical") > 0.1f)
        {

            if (currenttime > waittime)
            {
                changebtn(-1);
                currenttime = 0f;
            }
            else
            {
                currenttime += deltaTime;

            }
        }
        else
        {
            currenttime = 0f;
        }
		
	}
}
