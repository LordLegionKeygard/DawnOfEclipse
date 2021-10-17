using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    PotionType potionType;

    private HealthControl healthControl;

    private void Start()
    {
        healthControl = GetComponent<HealthControl>();
    }
    public void UsePotions()
    {
        Debug.Log("UsePotions");
        switch (potionType)
        {
            case (PotionType.Health):
                if (healthControl.currentHealth > healthControl.maxHealth - 50)
                {
                    healthControl.currentHealth = healthControl.maxHealth;
                }
                else
                {
                    healthControl.currentHealth += 50;
                }
                healthControl.UpdateHealthColorBar();

                break;

            case (PotionType.Speed):

                break;
        }
    }
}
