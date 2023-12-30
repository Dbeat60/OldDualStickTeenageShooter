using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWeaponManager : MonoBehaviour {
    public Transform currentshootpos;
    //public Transform LaserGun;
    public Transform ShootPos1;
    public Transform ShootPos2;
    public GameObject weaponPlayer1, weaponPlayer2,shotgun1, shieldPlayer1, shieldPlayer2,machineGun;
    public int[] bulletsp1,bulletsp2, currentbullets;
    float leftTrigger;
    float rightTrigger;
    public player.PlayerState PState;
    public bool pointing = true;
    public shootGun shootGun;
    GranadeWeapon GranadeLogic;
    LaserManager LM;
    public Weapons currentWeaponID =0;
    WaitForSeconds rumbleDuration = new WaitForSeconds(0.25f);
    bool rumbling = false;
    public bool canshoot=true;
    float timeSinceLastShoot = 0f;
   
    public float maxTimePointing = 3f;
    public static event Action<int> UpdateWeaponDelegate = delegate { };

    public enum Weapons
    {
        gun,
        shootgun,
        laser,
        machineGun
    }
    public void UpdateWeapon(int i)
    {
        UpdateWeaponDelegate(i);
    }
    public void updateCurrentWeapon(int i)
    {      
        currentWeaponID = (Weapons)i;
        switch(currentWeaponID)
        {
            case  Weapons.gun:
                shootGun.currentgun = shootGun.Gun;
                break;
            case Weapons.shootgun:
                shootGun.currentgun = shootGun.ShotGun;
                break;
            case Weapons.machineGun:
                shootGun.currentgun = shootGun.mGun;
                break;
        }
        ChangeCurrentWeaponObject();        
    }
    public void ChangeCurrentWeaponObject()
    {
        if (!shieldPlayer1.activeSelf)
        {
            if (currentWeaponID == Weapons.shootgun)
            {
                shotgun1.SetActive(true);
                weaponPlayer1.SetActive(false);
                machineGun.SetActive(false);


            }
            else if (currentWeaponID == Weapons.gun)
            {
                weaponPlayer1.SetActive(true);
                shotgun1.SetActive(false);
                machineGun.SetActive(false);

            }
            else if (currentWeaponID == Weapons.machineGun)
            {
                weaponPlayer1.SetActive(false);
                shotgun1.SetActive(false);
                machineGun.SetActive(true);

            }
             

        }
    }
 
    private void Start()
    {
        currentbullets = new int[Enum.GetNames(typeof(Weapons)).Length];
        bulletsp1 = new int[Enum.GetNames(typeof(Weapons)).Length];
        bulletsp2 = new int[Enum.GetNames(typeof(Weapons)).Length];
        bulletsp2[0]=bulletsp1[0] = int.MaxValue;
        bulletsp2[1]=bulletsp1[1] = 10;
        bulletsp2[2]=bulletsp1[2] = 99;
        bulletsp2[3] = bulletsp1[3] = 99;

        for (int i = 0; i < bulletsp1.Length; i++)
        {
            currentbullets[i] = bulletsp1[i];
        }

        UpdateWeaponDelegate += updateCurrentWeapon;
        
        PState = player.PlayerState.gun;
        shootGun = GetComponent<shootGun>();
        GranadeLogic = GetComponent<GranadeWeapon>();
        LM = GetComponentInChildren<LaserManager>();
        
    }
    
    public void Update()
    {
        Input();
        timeSinceLastShoot += Time.deltaTime;
        if (timeSinceLastShoot > maxTimePointing)
        {
            if (PState != player.PlayerState.shield)
                player.Instance.playerAnimationManager.IsPlayer1Pointing = false;
            else
                player.Instance.playerAnimationManager.IsPlayer2Pointing = false;
        }
          


    }
    int numplayer = 1;
    private void Input()
    {

        leftTrigger = LevelManager.Instance.Inputreader.getTrigger(true);
        rightTrigger = LevelManager.Instance.Inputreader.getTrigger(false);
        if (LevelManager.Instance.Inputreader.getButtonDown("B") || UnityEngine.Input.GetKeyDown(KeyCode.E))
        {

            player.Instance.callSwitchPlayerEvent();

            numplayer = (numplayer == 1) ? 2 : 1;


        }


        if (PState == player.PlayerState.gun)
            pointing = true;
        else
            pointing = false;



        GranadeLogic.Throwgranade(leftTrigger);
        if(numplayer ==1)
        { 
        if ((rightTrigger > 0 || UnityEngine.Input.GetMouseButtonDown(1)))
        {

                if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
                {
                    player.Instance.playerAnimationManager.Player1Anim.SetTrigger("aiming");
                }

                if (currentbullets[(int)currentWeaponID] <= 0)
                {
                    updateCurrentWeapon(0);
                }
                else
                {
                    if (canshoot && shootGun.canshoot)
                    {


                        timeSinceLastShoot = 0f;
                        if (PState != player.PlayerState.shield)
                        {
                            player.Instance.playerAnimationManager.IsPlayer1Pointing = true;
                        }
                        else
                        {
                            player.Instance.playerAnimationManager.IsPlayer2Pointing = true;
                        }

                        currentbullets[(int)currentWeaponID]--;
                        
                        shootGun.currentgun.Shot();
                        print("s");
                        rumble(0.12f,0.12f);
                        

                    }
                }
           
        }
        else
        {
            canshoot = true;
            if (currentWeaponID == Weapons.laser)
            {
                LM.resetLaser();
            }
        }
        }
        else
        {
            if ((rightTrigger > 0 || UnityEngine.Input.GetMouseButtonDown(1)))
            {
                player.Instance.ComboMgr.BaseAttack();
            }
        }

    }
    public void rumble(float rAmmount,float lAmmount)
    {
        if (!rumbling)
        {
            rumbling = true;
            Gamepad.current.SetMotorSpeeds(lAmmount,rAmmount);
            StartCoroutine(stopRummble());
        }
    }
    IEnumerator stopRummble()
    {
        yield return rumbleDuration;
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        rumbling = false;

    }
}
