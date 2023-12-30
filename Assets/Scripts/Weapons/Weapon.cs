using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class Weapon : MonoBehaviour {
    public bool CanShoot = true;
    private bool waitForTriggerUp=false;
    public bool isSingleShot = true;
    [SerializeField]
    [Range(0.01f, 2f)]
    float bulletcooloff = 0.5f;
    public GameObject shotEffectPlayer1;
    public float bulletspeed = 5000;

    public virtual void Shot()
    {

    }
    public IEnumerator shootcooloff()
    {

        yield return new WaitForSeconds(bulletcooloff);
        if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
            player.Instance.playerAnimationManager.Player1Anim.SetLayerWeight(2, 0f);
        if (isSingleShot)
        {
            if (LevelManager.Instance.Inputreader.getTrigger(false) <= 0)
                CanShoot = true;
            else
                waitForTriggerUp = true;
        }
        else
        {
            CanShoot = true;
        }
            
    }
     

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	public void Update ()
    {
        if(waitForTriggerUp)
        {
            if (LevelManager.Instance.Inputreader.getTrigger(false) <= 0)
            {
                waitForTriggerUp = false;
                CanShoot = true;
            }
        }
    }
}
