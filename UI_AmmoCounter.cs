using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AmmoCounter : MonoBehaviour
{
    public Text textDisplay;

    public void setCurrentAmmo(string ammo)
    {
        textDisplay.text = ammo;
    }
}
