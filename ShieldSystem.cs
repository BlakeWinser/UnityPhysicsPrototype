using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : MonoBehaviour
{
    public GameObject shield;
    public float shieldHealth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
    }

    void ShieldDamage()
    {
        //If shield has < 0 hp, break();
        //Start shield slow regen.
    }
}
