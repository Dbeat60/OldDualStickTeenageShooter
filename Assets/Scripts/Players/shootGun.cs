using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootGun : MonoBehaviour
{
   public bool canshoot = true;
     public float bulletspeed = 5000;
    public GameObject muzzleParticle1,muzzleParticle2;
    [SerializeField][Range(0.01f, 2f)]
    float bulletcooloff = 0.5f;
    float MGbulletcooloff = 0.05f;
    
    public gun Gun;
    public shotGun ShotGun;
    public MachineGun mGun;
    public Weapon currentgun;

    public void Start()
    {
        currentgun = Gun;
    }
    public void shoot()
    {
        currentgun.Shot();
    }
    public void shootshotGun()
    {
        currentgun = ShotGun;
        currentgun.Shot();
       //  ShotGun.Shot();
         
    }
    public void shootgun()
    {
        currentgun = Gun;
        currentgun.Shot();
       // Gun.Shot();
    }
    public void shootMachineGun()
    {
        mGun.Shot();
    }

    public void Update()
    { 
    }
  public  IEnumerator shootcooloff()
    {
     
        yield return new WaitForSeconds(bulletcooloff);
        if (player.Instance.playerWeaponManager.PState == player.PlayerState.gun)
            player.Instance.playerAnimationManager.Player1Anim.SetLayerWeight(2, 0f);


        canshoot = true;
    }

    public IEnumerator MGshootcooloff()
    {

        yield return new WaitForSeconds(MGbulletcooloff);         

        canshoot = true;
    }

    public IEnumerator deactivateMuzzle()
    {
        yield return new WaitForSeconds(0.325f);
        muzzleParticle1.SetActive(false);
    }
     
}
