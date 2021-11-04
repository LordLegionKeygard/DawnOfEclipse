using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUseManager : MonoBehaviour
{
    private StaminaControl staminaControl;
    void Start()
    {
        staminaControl = GetComponent<StaminaControl>();
    }
    public void Use50Stamina()
    {
        staminaControl.UseStamina(50);
    }

    public void Use100Stamina()
    {
        staminaControl.UseStamina(100);
    }
    public void Use150Stamina()
    {
        staminaControl.UseStamina(150);
    }
}
