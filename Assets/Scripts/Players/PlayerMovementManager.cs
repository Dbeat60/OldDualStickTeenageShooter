using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, 1f)]
    float slowdownModifier = 1;
    float speedmodifier = 1;
    [Range(0.001f, 150f)]
    public float movementspeed = 0.3f;
    [Range(0.001f, 150f)]
    public float rotationSpeed = 0.3f;
    public bool isplayer2;
    Rigidbody rb;
    public Vector3 deltadir;
    Vector3 prevmov;
    [Range(0f, 10f)]
    public float brakeforce;
    [Range(0, 25f)]
    public   float smokeSize=1f;
    public Vector3 smokerotation;
   // [HideInInspector]
    public bool canmove;
  
    private void Start()
    {
        canmove = true;
        rb = GetComponent<Rigidbody>();
        prevmov = Vector3.zero;
        hascontroller = (Input.GetJoystickNames().Length > 0);
    }
    public GameObject brakeEffect;
    bool canbrakeEffect = false;
    public bool ignoreMovement=false;
    public void CalculateMovement()
    {
        if (canmove )
        {
           // print(player.Instance.isJumping);
            if (LevelManager.Instance.Inputreader.getMovementVector() != Vector3.zero && LevelManager.Instance.Inputreader.getRotationVector() == Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(((!player.Instance.playerWeaponManager.pointing) ? -1 : 1) * LevelManager.Instance.Inputreader.getMovementVector()), Time.deltaTime * rotationSpeed);
                speedmodifier = 1f;
            }

            Vector3 movedir = LevelManager.Instance.Inputreader.getMovementVector().normalized * movementspeed * speedmodifier;
            movedir.y = rb.velocity.y;
            deltadir = movedir - prevmov;
          //  print("speed " + deltadir.magnitude);
            if (movedir.magnitude > 0  && !ignoreMovement)
            {
                if (player.Instance.playerAnimationManager.Player1Anim.GetFloat("speed") != 0)
                    rb.velocity = movedir;
                else 
                    rb.velocity = Vector3.zero;

                canbrakeEffect = true;
            }
            else 
            {
                if (canbrakeEffect)
                {
                    //canbrakeEffect = false;
                    //GameObject GO = Instantiate(brakeEffect, transform.position, transform.rotation );
                    //GO.transform.parent = player.Instance.effectPoint;
                    //ToonEffects TE = GO.GetComponent<ToonEffects>();
                    //StartCoroutine(TE.Detach());

                }
                rb.AddForce(transform.forward * brakeforce);
            }
            if (deltadir.x < 0 || deltadir.z < 0)
            {
                //print(movedir.normalized.magnitude);
                if (movedir.normalized.magnitude == 0)
                    player.Instance.playerAnimationManager.Player1Anim.SetTrigger("forceStop");
             
            }
            prevmov = movedir;
        }
        
    }
     public GameObject placeholder;
    public bool hascontroller;
    public void CalculateDirection()
    {        
        if (!hascontroller)
        {
            Vector3 mousedir;
            mousedir = new Vector3( Input.mousePosition.x,0, Input.mousePosition.y);
            mousedir.x -= Screen.width / 2f;
            mousedir.z -= Screen.height / 2f;

            mousedir.x = mousedir.x / (Screen.width / 2f);
            mousedir.z = mousedir.z/ (Screen.height / 2f);
            
            transform.rotation = Quaternion.LookRotation(((!player.Instance.playerWeaponManager.pointing) ? -1 : 1) * mousedir);

           /* RaycastHit rh;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousedir), out rh, 1000))
            {
                placeholder.transform.position = rh.point;
                Vector3 target = new Vector3(placeholder.transform.position.x, transform.position.y, placeholder.transform.position.z);
                transform.LookAt(target);
            }*/
        }
        else
        {
            if(LevelManager.Instance.Inputreader.getRotationVector() != Vector3.zero)
            {                
                transform.rotation = Quaternion.LookRotation(((!player.Instance.playerWeaponManager.pointing) ? -1 : 1) * LevelManager.Instance.Inputreader.getRotationVector());
                speedmodifier = slowdownModifier;
            }
        }
    }

    public void FixedUpdate()
    {
        if (player.Instance.playerAnimationManager.Player1Anim.GetFloat("speed") == 0 && !player.Instance.isJumping)
        {
            //rb.velocity = new Vector3(0, rb.velocity.y, 0)*Time.deltaTime;
        }
    }
}
