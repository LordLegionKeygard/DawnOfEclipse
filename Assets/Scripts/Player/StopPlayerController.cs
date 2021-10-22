using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerController : MonoBehaviour
{
    private PlayerController controller;
    private StaminaControl staminaControl;
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        staminaControl = GetComponent<StaminaControl>();
    }
    public void StopWalk()
    {
        controller.walk = false;
    }

    public void CanWalk()
    {
        controller.walk = true;
    }

    public void R1ComboMain()
    {
        staminaControl.UseStamina(100);
    }

    public void Roll()
    {
        controller.block = true;
        StartCoroutine(ExecuteAfterTime(1f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            controller.block = false;
        }
    }
}