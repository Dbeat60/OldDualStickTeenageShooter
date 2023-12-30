using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJumpController : MonoBehaviour
{
    // Start is called before the first frame update
    jumpManager jumpMG;
    void Start()
    {
        jumpMG = GetComponentInParent<jumpManager>();
    }
    public void JumpStart()
    {
        if(jumpMG)
        {
            jumpMG.JumpStart();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
