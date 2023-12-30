using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthManager : MonoBehaviour {
    public float Healthp1 = 0;
    public float RecoveryHealthF = 0;
    public float  HealthP2 = 0;
    public float CurrentHealthP1;
    public float CurrentHealthP2;
    public  float MaxHealth = 100f;
    public float Damage;
    public static event Action<float> UpdateHealth = delegate { };
    public float SubHealthRecoveryRatio = 2f;
    private void Awake()
    {
        Healthp1 = HealthP2 = MaxHealth;
        CurrentHealthP1 = Healthp1;
        CurrentHealthP2 = HealthP2;
    }
    public void Start()
    {
        UpdateHealth += UpdateHealthf;
    }
    public void setup(PlayerSettings HealthSettings)
    {
        Healthp1 = HealthSettings.maxHealth;
        RecoveryHealthF = Healthp1;    
    }
    public void switchPlayer(int i)
    {
 
        if (i == 0)
        {
            Healthp1 = CurrentHealthP1;
            CurrentHealthP1 = HealthP2;
            CurrentHealthP2 = Healthp1;
        }
        else
        {
            HealthP2 = CurrentHealthP1;
            CurrentHealthP1 = Healthp1;
            CurrentHealthP2 = HealthP2;
        }
    }
    public float getHealth()
    {
        return CurrentHealthP1;
    }
    public float getrecoveryHealth()
    {
        return RecoveryHealthF;
    }
    public float getHealthover100()
    {
        return CurrentHealthP1 / MaxHealth;
    }
    public float getHealth2over100()
    {
        print(CurrentHealthP2 / MaxHealth);
        return CurrentHealthP2 / MaxHealth;
    }
  public  IEnumerator recoverhealth(int currentplayer)
    {
        while (CurrentHealthP2 < getrecoveryHealth())
        {
            print(getrecoveryHealth());
            yield return new WaitForSeconds(1.5f);
            CurrentHealthP2 += 5f;
            if (currentplayer == 0)
            {
                HealthP2 = CurrentHealthP2;
            }
            else
            {
                Healthp1 = CurrentHealthP2;
            }

            UpdateHealth(0);
        }
        //if (CurrentHealthP2 < MaxHealth)
          //  StartCoroutine(recoverhealth(currentplayer));
    }
   
    public void UpdateHealthf(float healthamount)
    {
        Damage = healthamount;
        CurrentHealthP1 -= healthamount;
        RecoveryHealthF -= healthamount / SubHealthRecoveryRatio;
        if (CurrentHealthP1 <= 0)
        {
            //Game Pver
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);


        }
    }

    public void callUpdateHealth(float f)
    {
        if(f>0)
        {
            player.Instance.playerWeaponManager.rumble(.5f,0.5f);
        }
        UpdateHealth(f);
    }
}
