using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Public members

    public enum PowerUpType
    {
        Health,
        Shield,
        Power,
    }

    // Private members

    [SerializeField]
    private PowerUpType powerUpType;
    [Header("Instant power-ups")]
    [Header("Health")]
    [SerializeField]
    private float health;
    [Header("Temporary power-ups")]
    [SerializeField]
    private float duration;
    [SerializeField]
    private float power;

    // Public methods

    public void Apply()
    {
        if (powerUpType == PowerUpType.Health)
        {
            //player.Instance.playerHealthManager.Heal(health);
        }
        else if (powerUpType == PowerUpType.Shield)
        {
            //player.Instance.playerHealthManager.SetInvulnerability(duration);
        }
        else if (powerUpType == PowerUpType.Power)
        {
            //player.Instance.SetPower(power, duration);
        }
    }

}
