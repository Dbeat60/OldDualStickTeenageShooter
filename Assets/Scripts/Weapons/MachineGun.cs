using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon {

	// Use this for initialization
	void Start () {
		
	}
    public override void Shot()
    {

        if (CanShoot)
        {
            if (player.Instance.playerWeaponManager.PState != player.PlayerState.gun)
                player.Instance.playerAnimationManager.Player2Anim.SetTrigger("attack");


            player.Instance.playerAudioManager.ShootBullet.Play();
            player.Instance.playerWeaponManager.currentbullets[(int)player.Instance.playerWeaponManager.currentWeaponID]--;
            CanShoot = false;
            StartCoroutine(shootcooloff());
            GameObject b = PoolSystem.Instance.getFromPool(true);
            b.transform.position = player.Instance.playerWeaponManager.currentshootpos.position;
            b.transform.rotation = player.Instance.transform.rotation;

            b.GetComponent<ProjectileAC>().shoot(player.Instance.playerWeaponManager.currentshootpos.forward * bulletspeed);
            if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
            {
                shotEffectPlayer1.SetActive(true);
                Instantiate(shotEffectPlayer1, player.Instance.playerWeaponManager.currentshootpos.position, Quaternion.identity);

            }

        }
    }
        // Update is called once per frame
        void Update () {
		
	}
}
