using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    public static bool CanDrinkAnyPotions = true;
    [SerializeField] private ParticleSystem speedPotionParticle;
    private HealthControl healthControl;
    private PlayerAnimatorManager playerAnimatorManager;
    public float potionSpeed;
    public bool speedPotion = false;

    private void OnEnable()
    {
        CustomEvents.OnUsePotion += UsePotions;
    }
    private void Start()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        healthControl = GetComponent<HealthControl>();
    }
    private void UsePotions(int potion)
    {
        CantDrinkAnyPotions();
        StartCoroutine(ExecuteAfterTime1(1.8f));
        IEnumerator ExecuteAfterTime1(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            switch (potion)
            {
                case (0):
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

                case (1):
                    StopCoroutine(ExecuteAfterTime(0f));
                    speedPotionParticle.Play();
                    potionSpeed = 5;
                    speedPotion = true;

                    StartCoroutine(ExecuteAfterTime(20f));
                    IEnumerator ExecuteAfterTime(float timeInSec)
                    {
                        yield return new WaitForSeconds(timeInSec);
                        speedPotionParticle.Stop();
                        potionSpeed = 0;
                        speedPotion = false;
                    }
                    break;
                case (2):
                    break;
            }
        }
    }

    private void CantDrinkAnyPotions()
    {
        CanDrinkAnyPotions = false;
        StartCoroutine(ExecuteAfterTime(3f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            CanDrinkAnyPotions = true;
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnUsePotion -= UsePotions;
    }
}
