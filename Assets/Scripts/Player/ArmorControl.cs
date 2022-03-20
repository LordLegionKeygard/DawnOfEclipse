using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmorControl : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _armorText;
    public int CurrentArmor = 0;
    public int TorsoArmor;
    public int HandRightArmor;
    public int HandLeftArmor;
    public int ArmUpperRightArmor;
    public int ArmUpperLeftArmor;
    public int ArmLowerRightArmor;
    public int ArmLowerLeftArmor;
    public int HipsArmor;
    public int LegLeftArmor;
    public int LegRightArmor;
    public int BackAttachmentArmor;
    public int ShoulderLeftArmor;
    public int ShoulderRightArmor;
    public int HeadSlotArmor;
    public int ElbowRightArmor;
    public int ElbowLeftArmor;
    public int KneeRightArmor;
    public int KneeLeftArmor;
    public int ShieldArmorPassive;
    public int ShieldBlockArmor;
    public int ShieldBlockArmorDefault;

    public void ResetArmor()
    {
        BackAttachmentArmor = 0;
        ShoulderLeftArmor = 0;
        ShoulderRightArmor = 0;
        HeadSlotArmor = 0;
        ElbowRightArmor = 0;
        ElbowLeftArmor = 0;
        KneeRightArmor = 0;
        KneeLeftArmor = 0;
        ShieldArmorPassive = 0;
        ShieldBlockArmor = 0;
        ShieldBlockArmorDefault = 0;
        UpdateArmor();
    }

    public void ShieldBlock()
    {
        ShieldBlockArmor = ShieldBlockArmorDefault;
        UpdateArmor();
    }

    public void ShieldUnBlock()
    {
        ShieldBlockArmor = 0;
        UpdateArmor();
    }

    public void UpdateArmor()
    {
        CurrentArmor = TorsoArmor +
        HandRightArmor +
        HandLeftArmor +
        ArmUpperRightArmor +
        ArmUpperLeftArmor +
        ArmLowerRightArmor +
        ArmLowerLeftArmor +
        HipsArmor +
        LegLeftArmor +
        LegRightArmor +
        BackAttachmentArmor +
        ShoulderLeftArmor +
        ShoulderRightArmor +
        HeadSlotArmor +
        ElbowLeftArmor +
        ElbowRightArmor +
        KneeRightArmor +
        KneeLeftArmor +
        ShieldArmorPassive +
        ShieldBlockArmor;

        _armorText.text = (CurrentArmor.ToString());
    }
}
