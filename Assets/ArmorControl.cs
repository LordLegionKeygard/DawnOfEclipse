using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorControl : MonoBehaviour
{

    [SerializeField] private Text armorText;
    public int currentArmor = 0;
    public int torsoArmor;
    public int handRightArmor;
    public int handLeftArmor;
    public int armUpperRightArmor;
    public int armUpperLeftArmor;
    public int armLowerRightArmor;
    public int armLowerLeftArmor;
    public int hipsArmor;
    public int legLeftArmor;
    public int legRightArmor;
    public int backAttachmentArmor;
    public int shoulderLeftArmor;
    public int shoulderRightArmor;
    public int headSlotArmor;
    public int elbowRightArmor;
    public int elbowLeftArmor;
    public int kneeRightArmor;
    public int kneeLeftArmor;
    public int shieldArmorPassive;
    public int shieldBlockArmor;
    public int shieldBlockArmorDefault;

    public void ResetArmor()
    {

    }

    public void ShieldBlock()
    {
        shieldBlockArmor = shieldBlockArmorDefault;
        UpdateArmor();
    }

    public void ShieldUnBlock()
    {
        shieldBlockArmor = 0;
        UpdateArmor();
    }

    public void UpdateArmor()
    {
        currentArmor = torsoArmor +
        handRightArmor +
        handLeftArmor +
        armUpperRightArmor +
        armUpperLeftArmor +
        armLowerRightArmor +
        armLowerLeftArmor +
        hipsArmor +
        legLeftArmor +
        legRightArmor +
        backAttachmentArmor +
        shoulderLeftArmor +
        shoulderRightArmor +
        headSlotArmor +
        elbowLeftArmor +
        elbowRightArmor +
        kneeRightArmor +
        kneeLeftArmor +
        shieldArmorPassive +
        shieldBlockArmor;

        armorText.text = ("Armor: " + currentArmor.ToString());
    }
}
