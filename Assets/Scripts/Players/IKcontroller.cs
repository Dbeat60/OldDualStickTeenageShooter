﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKcontroller : MonoBehaviour {

    [SerializeField]
    protected Animator animator;

    private bool pointing = true;
    private Transform rightHandObj = null;
    private Transform lookObj = null;
    private Transform forwardTransform;
    public Vector3 Offset;
    [HideInInspector]
    public PlayerWeaponManager PlayerWeaponManager;
    
    void Start()
    {
        rightHandObj = lookObj = transform.GetChild(0);
        forwardTransform = transform.GetChild(1);
       
        PlayerWeaponManager = GetComponent<PlayerWeaponManager>();

        
    }



    private void Update()
    {

        if (pointing)
        {
            rightHandObj.position =  forwardTransform.position + forwardTransform.forward * Offset.z + transform.up * Offset.y;


        }
    }





        //a callback for calculating IK
        void OnAnimatorIK()
    {
        if (animator)
        {
            
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if (PlayerWeaponManager.pointing)
            {
              
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
 
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
               
            }
        }
    }
}
