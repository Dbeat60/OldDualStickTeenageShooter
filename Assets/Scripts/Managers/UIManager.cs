using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    [SerializeField]
    Text scoreHolder, Ammo;
    [SerializeField]
    Image currentWeapon, Life,backLife,damagelife,recoverlife, Life2,backLife2, activePlayer,passivePlayer;
    [SerializeField] Sprite p1Active, p2Pasive, p1Pasive, p2Active;
    [SerializeField]
    Sprite[] weapons;

    // Use this for initialization
    void Start ()
    {
        LevelManager.UpdateScore += updateScoreUI;
        PlayerWeaponManager.UpdateWeaponDelegate+= UpdateWeaponUI;
        playerHealthManager.UpdateHealth += UpdateHealth;
        player.switchPlayer += switchPlayer;
        UpdateWeaponUI(0);

	}

    public void UpdateWeaponUI(int type)
    {
        currentWeapon.sprite = weapons[type];

        if (player.Instance.playerWeaponManager.currentWeaponID != PlayerWeaponManager.Weapons.gun)
        {
            string b = player.Instance.playerWeaponManager.currentbullets[type].ToString();
            if (b.Length > 1)
                b = b[0] + " " + b[1];
            Ammo.text =b;
        }
        else Ammo.text = "   ∞";   



    }
    void switchPlayer()
    {
        if(player.Instance.playerWeaponManager.PState == player.PlayerState.shield)
        {
            activePlayer.sprite = p2Active;
            passivePlayer.sprite = p1Pasive;
        }
        else
        {
            activePlayer.sprite = p1Active;
            passivePlayer.sprite = p2Pasive;
        }
        Life.fillAmount = player.Instance.playerHealthManager.getHealthover100();
        Life2.fillAmount = player.Instance.playerHealthManager.getHealth2over100();

    }
    void UpdateHealth(float i)
    {
        if (Life && damagelife && recoverlife && Life2)
        {
            Life.fillAmount = player.Instance.playerHealthManager.getHealthover100();
            damagelife.fillAmount = Life.fillAmount + player.Instance.playerHealthManager.Damage / 100f;
            recoverlife.fillAmount = player.Instance.playerHealthManager.getrecoveryHealth() / 100f;

            Life2.fillAmount = player.Instance.playerHealthManager.getHealth2over100();
        }
    }
    public void updateScoreUI(int i)
    {
        if(scoreHolder)
         scoreHolder.text ="Score  "+ LevelManager.Instance.score.ToString();
    }
	// Update is called once per frame
	void Update ()
    {
        if (damagelife.fillAmount > 0f)
            damagelife.fillAmount -= Time.deltaTime / 5f;
		
	}
}
