using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeCooldown : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public void NoWeapon()
    {
        playerController.timeR1 = 1f;
        playerController.timeR2 = 2f;
        playerController.timeL1 = 1.6f;
        playerController.timeR1FastRun = 1f;
    }
    public void GreatSword()
    {
        playerController.timeR1 = 0.8f;
        playerController.timeR2 = 3.5f;
        playerController.timeL1 = 0.7f;
        playerController.timeR1FastRun = 1.5f;
    }
    public void LongSword()
    {
        playerController.timeR1 = 0.8f;
        playerController.timeR2 = 1.5f;
        playerController.timeL1 = 0.7f;
        playerController.timeR1FastRun = 1.5f;
    }
}
