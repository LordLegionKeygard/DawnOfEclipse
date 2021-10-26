using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorControl : MonoBehaviour
{
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

    public void ResetArmor()
    {
        
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
        kneeLeftArmor;
    }
}
