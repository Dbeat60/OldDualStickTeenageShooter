using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchPlayer : MonoBehaviour {
    bool canChangePlayer = true;
    

    [SerializeField] float coolOffChangePlayerTime = 1.5f;

    private void Start()
    {
        player.Instance.playerWeaponManager.currentshootpos = player.Instance.playerWeaponManager.ShootPos1;
        //player.Instance.playerWeaponManager.LaserGun.parent = player.Instance.playerWeaponManager.currentshootpos;
        //player.Instance.playerWeaponManager.LaserGun.localPosition = Vector3.zero;
        player.switchPlayer += Switchweapon;
        player.Instance.playerWeaponManager.currentshootpos = player.Instance.playerWeaponManager.ShootPos1;
        //player.Instance.playerWeaponManager.LaserGun.parent = player.Instance.playerWeaponManager.currentshootpos;
        //player.Instance.playerWeaponManager.LaserGun.localPosition = Vector3.zero;
        player.switchPlayer += Switchweapon;
    }
    IEnumerator coolOffChangeplayer()
    {
        yield return new WaitForSeconds(coolOffChangePlayerTime);
        canChangePlayer = true;
    }
    public void Switchweapon()
    {
        if (canChangePlayer)
        {
            canChangePlayer = false;
            StartCoroutine(coolOffChangeplayer());
            if (player.Instance.playerWeaponManager.pointing)
            {
                player.Instance.playerWeaponManager.currentshootpos.gameObject.SetActive(false);// = player.Instance.playerWeaponManager.ShootPos2;
                player.Instance.playerWeaponManager.weaponPlayer1.SetActive(false);
                player.Instance.playerWeaponManager.shieldPlayer1.SetActive(true);
                player.Instance.playerWeaponManager.weaponPlayer2.SetActive(true);
                player.Instance.playerWeaponManager.shieldPlayer2.SetActive(false);
                player.Instance.playerWeaponManager.pointing = false;
                player.Instance.playerWeaponManager.PState = player.PlayerState.shield;
                for (int i = 0; i < player.Instance.playerWeaponManager.bulletsp1.Length; i++)
                {
                    player.Instance.playerWeaponManager.bulletsp1[i] = player.Instance.playerWeaponManager.currentbullets[i];
                }
                for (int i = 0; i < player.Instance.playerWeaponManager.bulletsp1.Length; i++)
                {
                    player.Instance.playerWeaponManager.currentbullets[i] = player.Instance.playerWeaponManager.bulletsp2[i];
                }
            }
            else
            {
                player.Instance.playerWeaponManager.currentshootpos.gameObject.SetActive(true);                 
                player.Instance.playerWeaponManager.weaponPlayer1.SetActive(true);
                player.Instance.playerWeaponManager.shieldPlayer1.SetActive(false);
                player.Instance.playerWeaponManager.weaponPlayer2.SetActive(false);
                player.Instance.playerWeaponManager.shieldPlayer2.SetActive(true);
                player.Instance.playerWeaponManager.pointing = true;
                player.Instance.playerWeaponManager.PState = player.PlayerState.gun;
                for (int i = 0; i < player.Instance.playerWeaponManager.bulletsp2.Length; i++)
                {
                    player.Instance.playerWeaponManager.bulletsp2[i] = player.Instance.playerWeaponManager.currentbullets[i];
                }
                for (int i = 0; i < player.Instance.playerWeaponManager.bulletsp2.Length; i++)
                {
                    player.Instance.playerWeaponManager.currentbullets[i] = player.Instance.playerWeaponManager.bulletsp1[i];
                }
            }
            player._Instance.playerWeaponManager.UpdateWeapon((int)player.Instance.playerWeaponManager.currentWeaponID);
        }
    }
}
