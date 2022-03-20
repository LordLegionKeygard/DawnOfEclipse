using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicArmorControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magicArmorText;
    public int CurrentMagicArmor = 0;
    public int LeftRingMagicArmor;
    public int RightRingMagicArmor;

    public void ResetArmor()
    {
        LeftRingMagicArmor = 0;
        RightRingMagicArmor = 0;
        UpdateMagicArmor();
    }

    public void UpdateMagicArmor()
    {
        CurrentMagicArmor = LeftRingMagicArmor +
        RightRingMagicArmor;

        _magicArmorText.text = (CurrentMagicArmor.ToString());
    }
}
