using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

    [SerializeField]
    private bool has2Players = true;

    public Animator Player2Anim;
    public Animator Player1Anim;
    private void Awake()
    {
        Player2Anim.SetLayerWeight(1, 0f);
    }
    public bool IsPlayer1Pointing;
    public bool IsPlayer2Pointing;
    public void CalculateAnimations(float speed)
    {

        if (player.Instance.playerWeaponManager.PState == player.PlayerState.shield)
        {
            Player1Anim.SetBool("hasshield", true);
            Player2Anim.SetBool("hasshield", false);
        }
        else
        {
            Player1Anim.SetBool("hasshield", false);
            Player2Anim.SetBool("hasshield", true);

        }
        if (IsPlayer1Pointing && (int)player.Instance.playerWeaponManager.currentWeaponID == 0 && player.Instance.playerWeaponManager.PState != player.PlayerState.shield)
        {

            Player1Anim.SetLayerWeight(1, 1f);
            Player2Anim.SetLayerWeight(1, 0f);

        }
        else
        {
            Player1Anim.SetLayerWeight(1, 0f);

        }
       
        if (LevelManager.Instance.Inputreader.getRotationVector().magnitude != 0 && LevelManager.Instance.Inputreader.getMovementVector().magnitude != 0)
        {
          speed *= Vector3.Dot(LevelManager.Instance.Inputreader.getRotationVector(), LevelManager.Instance.Inputreader.getMovementVector());
        }

        Player2Anim.SetFloat("speed", speed);        
        Player1Anim.SetFloat("speed", speed);
        Player1Anim.SetInteger("Guntype", (int)player.Instance.playerWeaponManager.currentWeaponID);
        Player2Anim.SetInteger("Guntype", (int)player.Instance.playerWeaponManager.currentWeaponID);

    }
   
}
