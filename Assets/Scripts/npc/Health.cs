using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health :MonoBehaviour{

    [SerializeField]
    Image lifeBar;
    protected int health;
    [Range(0, 100)]
    [SerializeField]
    protected int maxHealth = 100;

    public virtual void SetupHealth()
    {
        health = maxHealth;
        UpdateHealthbar();
    }

    public virtual void AffectHealth(int hdelta)
    {
        health -= hdelta;
       
        UpdateHealthbar();
    }

    public virtual void RegainHealth(int hdelta)
    {
        health += hdelta;
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        if (lifeBar != null)
        {
            lifeBar.fillAmount = (float)health / (float)maxHealth;
        }

    }




}
