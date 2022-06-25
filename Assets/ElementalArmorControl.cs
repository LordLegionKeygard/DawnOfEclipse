using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementalArmorControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _elementalArmorText;
    public int[] ElementalArmor;

    private void OnEnable()
    {
        CustomEvents.OnElementalArmorBuff += ChangeElementalArmor;
    }

    public void ChangeElementalArmor(int _elementalArmorNumber, int amount)
    {
        ElementalArmor[_elementalArmorNumber] += amount;
        _elementalArmorText[_elementalArmorNumber].text = ElementalArmor[_elementalArmorNumber].ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnElementalArmorBuff -= ChangeElementalArmor;
    }
}
