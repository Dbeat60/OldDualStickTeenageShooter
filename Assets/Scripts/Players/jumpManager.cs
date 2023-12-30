using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpManager : MonoBehaviour
{
    private Rigidbody rb;
    private bool canRoll;
    [Range(0, 2f)]   
    private GameObject SmokeEffectPrefab;
    [SerializeField]
    [Range(0, 1f)]
    private float firstEffectDuration;
    private Vector3 jumpDir;

    public float CoolOffTime; 
    public float PreWarmTime;
    public float JumpForce = 5f;

    public GameObject SmokeRoll;
    public GameObject StartEffect;
    public GameObject MidEffect;
    IEnumerator CreateMidEffect()
    {
        yield return new WaitForSeconds(firstEffectDuration);
        MidEffect.SetActive(true);
        for (int i = 0; i < MidEffect.transform.childCount; i++)
        {
            MidEffect.transform.GetChild(i).GetComponent<ToonEffects>().
                StartCoroutine(MidEffect.transform.GetChild(i).GetComponent<ToonEffects>().Dissapear());

        }
    }
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        canRoll = true;
    }
	
    
	// Update is called once per frame
	void Update ()
    {
        if (rb.velocity.magnitude > 1f)
        {
             if (Input.GetKeyDown(KeyCode.LeftShift) || LevelManager.Instance.Inputreader.getButtonDown("A"))
            {
                
                if (LevelManager.Instance.Inputreader.getMovementVector().normalized.magnitude > 0)
                {
                    player.Instance.playerMovementManager.canmove = false;
                    player.Instance.playerAnimationManager.IsPlayer1Pointing = false;
                    player.Instance.playerWeaponManager.shootGun.canshoot = false;

                    jumpDelayed(LevelManager.Instance.Inputreader.getMovementVector().normalized);
                }
            }
        }
	}
    IEnumerator rollCooloff()
    {
        player.Instance.isJumping = false;
        yield return new WaitForSeconds(CoolOffTime);
         
        StartCoroutine(player.Instance.playerWeaponManager.shootGun.shootcooloff());
        canRoll = true;
       
        player.Instance.playerMovementManager.canmove = true;

    }
    IEnumerator WaitAndJump(Vector3 jumpDir)
    {
        this.jumpDir = jumpDir;
        yield return new WaitForSeconds(PreWarmTime);
        CreateEffect();
        jumpInmediatly(jumpDir);

    }
    public void JumpStart()
    {
        StartEffect.SetActive(true);
        for (int i = 0; i < StartEffect.transform.childCount; i++)
        {
            StartEffect.transform.GetChild(i).GetComponent<ToonEffects>().
                StartCoroutine(StartEffect.transform.GetChild(i).GetComponent<ToonEffects>().Dissapear());

        }
        StartCoroutine(CreateMidEffect());
        rb.AddForce(jumpDir * JumpForce, ForceMode.Impulse);
       

        StartCoroutine(rollCooloff());
    }
    #region effects
    private void CreateEffect()
    {
        SmokeEffectPrefab = Instantiate(SmokeRoll, player.Instance.effectPoint.position, transform.rotation);
        SmokeEffectPrefab.transform.parent = transform;
        
        ToonEffects TE = SmokeEffectPrefab.GetComponent<ToonEffects>();
        StartCoroutine(TE.Detach());
         

         
    }
    
    #endregion

    private void jumpDelayed(Vector3 jumpDir)
    {
        if (canRoll)
        {
            StartCoroutine(WaitAndJump(jumpDir));

        }
    }
    private void jumpInmediatly(Vector3 jumpDir)
    {
        canRoll = false;
        player.Instance.isJumping = true;

        player.Instance.playerAnimationManager.Player2Anim.SetTrigger("Roll");

        player.Instance.playerAnimationManager.Player1Anim.SetTrigger("Roll");

        //print(jumpDir);

       
    }
}
