using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorControl : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _armorText;
    public int CurrentArmor = 0;
    public int TorsoArmor;
    public int HandsArmor;
    public int ArmUppers;
    public int ArmLowers;
    public int HipsArmor;
    public int LegsArmor;
    public int Shoulders;
    public int HeadSlotArmor;
    public int Elbows;
    public int Knees;
    public int ShieldArmorPassive;
    public int ShieldBlockArmor;
    public int ShieldBlockArmorDefault;

    private void OnEnable()
    {
        CustomEvents.OnBlock += BlockToggle;
    }

    public void ResetArmor()
    {
        Shoulders = 0;
        HeadSlotArmor = 0;
        Elbows = 0;
        Knees = 0;
        ShieldArmorPassive = 0;
        ShieldBlockArmor = 0;
        ShieldBlockArmorDefault = 0;
        UpdateArmor();
    }

    private void BlockToggle(bool block)
    {
        if (block)
            ShieldBlockArmor = ShieldBlockArmorDefault;
        else
            ShieldBlockArmor = 0;

        UpdateArmor();
    }

    public void UpdateArmor()
    {
        var armor = TorsoArmor + HandsArmor + ArmUppers + ArmLowers + HipsArmor + LegsArmor + Shoulders + HeadSlotArmor + Elbows + Knees + ShieldArmorPassive + ShieldBlockArmor;
        CurrentArmor = (int)(((4 + armor) * (float)(ExperienceControl.CurrentLevel + 89) / 100));
        _armorText.text = CurrentArmor.ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnBlock -= BlockToggle;
    }
}
