using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour {

     
    [SerializeField]
    private GameObject pauseMenu;
    private bool pause;
    public bool IsPaused()
    {
        return Input.GetButtonDown("StartBtn");
            
    }
    public void SetPause()
    {
        if (IsPaused())
        {
            if (pauseMenu == null)
            {
                pauseMenu = GameObject.Find("MenuCanvas").transform.GetChild(1).gameObject;

            }

            pause = !pause;
            pauseMenu.SetActive(pause);

            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    public Vector3 getMovementVector()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
    }
    public Vector3 mousedir = Vector3.zero;
    public Vector3 getRotationVector()
    {
        mousedir = Input.mousePosition;
        mousedir.x -= Screen.width / 2f;
        mousedir.y -= Screen.height / 2f;

        mousedir.x = mousedir.x / (Screen.width / 2f);
        mousedir.y = mousedir.y / (Screen.height/ 2f);


        return new Vector3(Input.GetAxis("Horizontal2"), 0,
            Input.GetAxis("Vertical2"));

    }

    public float getTrigger(bool left)
    {
         
        return (left) ?  Input.GetAxis("LeftTrigger") :   Input.GetAxis("RightTrigger");
       
    }
    // if (Input.GetButtonDown("Bbtn"))

    public bool getButtonDown(string buttonLetter)
    {
        return Input.GetButtonDown(buttonLetter + "btn");
    }

    public float getSpeed()
    {
        return getMovementVector().normalized.magnitude+getRotationVector().normalized.magnitude;
    }
    
}
