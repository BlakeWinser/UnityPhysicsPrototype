using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnockbackDisplay : MonoBehaviour
{
    public Text textDisplay;

    public void SetMaxKnockback(float knockback)
    {
        textDisplay.text = knockback.ToString() + "%";
    }

    public void SetCurrentKnockback(float knockback)
    {
        textDisplay.text = knockback.ToString() + "%";
    }
}
