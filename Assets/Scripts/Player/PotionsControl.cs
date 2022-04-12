using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    public static bool CanDrinkAnyPotions = true;
    [SerializeField] private ParticleSystem _speedPotionParticle;
    private HealthControl _healthControl;
    private PlayerAnimatorManager _playerAnimatorManager;
    public float PotionSpeed;
    public bool SpeedPotion = false;

    private void OnEnable()
    {
        CustomEvents.OnUsePotion += UsePotions;
    }
    private void Start()
    {
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _healthControl = GetComponent<HealthControl>();
    }
    private void UsePotions(int potion)
    {
        if(!CanDrinkAnyPotions) return;
        CantDrinkAnyPotions();
        StartCoroutine(ExecuteAfterTime1(1.8f));
        IEnumerator ExecuteAfterTime1(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            switch (potion)
            {
                case (0):
                    if (_healthControl.CurrentHealth > _healthControl.MaxHealth - 50)
                    {
                        _healthControl.CurrentHealth = _healthControl.MaxHealth;
                    }
                    else
                    {
                        _healthControl.CurrentHealth += 50;
                    }
                    _healthControl.UpdateHealthColorBar();
                    break;

                case (1):
                    StopCoroutine(ExecuteAfterTime(0f));
                    _speedPotionParticle.Play();
                    PotionSpeed = 5;
                    SpeedPotion = true;

                    StartCoroutine(ExecuteAfterTime(20f));
                    IEnumerator ExecuteAfterTime(float timeInSec)
                    {
                        yield return new WaitForSeconds(timeInSec);
                        _speedPotionParticle.Stop();
                        PotionSpeed = 0;
                        SpeedPotion = false;
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
