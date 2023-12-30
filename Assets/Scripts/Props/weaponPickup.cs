using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{

    PlayerWeaponManager weaponManager;
    [SerializeField]
    PlayerWeaponManager.Weapons WeaponType;
     
    // Use this for initialization
    void Start()
    {
        weaponManager = GameObject.FindObjectOfType<PlayerWeaponManager>();
        if (!weaponManager)
        {
            Debug.LogError("NO  PLAYER IN SCENE!");
            Destroy(gameObject);

        }
        PlayerWeaponManager.UpdateWeaponDelegate+= updateWeaponType;
    }
    public void updateWeaponType(int i)
    {
         
    }
    private void OnTriggerEnter(Collider other)
    {
         
        if(other.tag=="Player")
        {
            if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
                player.Instance.playerAnimationManager.Player1Anim.SetTrigger("changeW");
            else
                player.Instance.playerAnimationManager.Player2Anim.SetTrigger("changeW");

            if (!weaponManager)
            {
                Debug.LogError("NO  PLAYER IN SCENE!");
                Destroy(gameObject);

            }
            weaponManager.updateCurrentWeapon((int)WeaponType);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
