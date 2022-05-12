using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUseManager : MonoBehaviour
{
    [SerializeField] private WeaponsInfo _weaponsInfo;
    private StaminaControl _staminaControl;

    private void Start()
    {
        _staminaControl = GetComponent<StaminaControl>();
    }
    public void UseStamina()
    {
        _staminaControl.UseStamina(_weaponsInfo.StaminaAttack);
    }
}
