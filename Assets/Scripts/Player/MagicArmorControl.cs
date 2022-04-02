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
    public int LeftEarringMagicArmor;
    public int RightEarringMagicArmor;
    public int NecklaceMagicArmor;

    public void ResetArmor()
    {
        LeftRingMagicArmor = 0;
        RightRingMagicArmor = 0;
        LeftEarringMagicArmor = 0;
        RightEarringMagicArmor = 0;
        NecklaceMagicArmor = 0;
        UpdateMagicArmor();
    }

    public void UpdateMagicArmor()
    {
        CurrentMagicArmor = LeftRingMagicArmor +
        RightRingMagicArmor +
        LeftEarringMagicArmor +
        RightEarringMagicArmor +
        NecklaceMagicArmor;

        _magicArmorText.text = (CurrentMagicArmor.ToString());
    }
}
