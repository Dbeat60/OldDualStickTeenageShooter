using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : Weapon {


    public override void Shot()
    {
        if (CanShoot)
        {
            if (player.Instance.playerWeaponManager.PState != player.PlayerState.gun)
                player.Instance.playerAnimationManager.Player2Anim.SetTrigger("attack");


            player.Instance.playerAudioManager.ShootBullet.Play();
            CanShoot = false;
            StartCoroutine(shootcooloff());
            if (!PoolSystem.Instance)
                Instantiate(player.Instance.poolManagerPrefab, Vector3.zero, Quaternion.identity);
            GameObject b = PoolSystem.Instance.getFromPool(true);
            b.transform.position = player.Instance.playerWeaponManager.currentshootpos.position;
            b.transform.rotation = player.Instance.transform.rotation;

            b.GetComponent<ProjectileAC>().shoot(player.Instance.playerWeaponManager.currentshootpos.forward * bulletspeed);
            if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
            {
               Instantiate(shotEffectPlayer1, player.Instance.playerWeaponManager.currentshootpos.position,Quaternion.identity);
            }

        }
    }
	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
