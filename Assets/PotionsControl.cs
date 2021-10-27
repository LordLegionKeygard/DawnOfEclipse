using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedPotionParticle;
    PotionType potionType;
    private HealthControl healthControl;
    private PlayerAnimatorManager playerAnimatorManager;
    public float potionSpeed;
    public bool speedPotion = false;
    private void Start()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        healthControl = GetComponent<HealthControl>();
    }
    public void UsePotions(int potion)
    {
        StartCoroutine(ExecuteAfterTime1(1.8f));
        IEnumerator ExecuteAfterTime1(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            switch (potion)
            {
                case (1):
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

                case (2):
                    StopAllCoroutines();
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
                case (3):
                    break;
            }
        }
    }
}
