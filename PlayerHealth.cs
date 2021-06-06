using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Variables
    public float currentHP;
    public float maxHP;

    //UI
    //public KnockbackDisplay knockbackDisplay;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player has died");

        Destroy(gameObject);
    }

}
