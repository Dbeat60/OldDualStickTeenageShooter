using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Weapon {

    public override void Shot()
    {
        if (CanShoot)
        {
            player.Instance.playerAudioManager.ShootBullet.Play();
            CanShoot = false;
            StartCoroutine(shootcooloff());
            if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
            {
                player.Instance.playerAnimationManager.Player1Anim.SetTrigger("shoot");
                player.Instance.playerAnimationManager.Player1Anim.SetLayerWeight(2, 1f);
            }
            else
                player.Instance.playerAnimationManager.Player2Anim.SetTrigger("attack");
            GameObject b = PoolSystem.Instance.getFromPool(true);
            b.transform.position = player.Instance.playerWeaponManager.ShootPos1.position;
            b.transform.rotation = player.Instance.playerWeaponManager.ShootPos1.rotation;
            b.GetComponent<ProjectileAC>().shoot(player.Instance.playerWeaponManager.currentshootpos.forward * bulletspeed);
            GameObject b1 = PoolSystem.Instance.getFromPool(true);
            b1.transform.position = player.Instance.playerWeaponManager.currentshootpos.position;
            b1.transform.rotation = player.Instance.playerWeaponManager.currentshootpos.rotation;
            b1.GetComponent<ProjectileAC>().shoot(Vector3.Lerp(player.Instance.playerWeaponManager.currentshootpos.forward, player.Instance.playerWeaponManager.currentshootpos.right, 0.125f) * bulletspeed);
            GameObject b2 = PoolSystem.Instance.getFromPool(true);
            b2.transform.position = player.Instance.playerWeaponManager.currentshootpos.position;
            b2.transform.rotation = player.Instance.playerWeaponManager.currentshootpos.rotation; ;
            b2.GetComponent<ProjectileAC>().shoot(Vector3.Lerp(player.Instance.playerWeaponManager.currentshootpos.forward, -player.Instance.playerWeaponManager.currentshootpos.right, 0.125f) * bulletspeed);

        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
