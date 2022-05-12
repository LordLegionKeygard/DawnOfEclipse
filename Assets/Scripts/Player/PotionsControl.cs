using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    public static bool CanDrinkAnyPotions = true;
    [SerializeField] private ParticleSystem _speedPotionParticle;
    [SerializeField] private HealthControl _healthControl;
    public float PotionSpeed;
    public bool SpeedPotion = false;
    [SerializeField] private int _healPointfromPotion;

    [SerializeField] private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        CustomEvents.OnUsePotion += UsePotions;
    }

    public void CalculateHealFromPotion()
    {
        _healPointfromPotion = _healthControl.MaxHealth / 3;
    }
    private void UsePotions(int potion)
    {
        if (!CanDrinkAnyPotions) return;
        CantDrinkAnyPotions();

        switch (potion)
        {
            case (0):
                if (_healthControl.CurrentHealth > _healthControl.MaxHealth - _healPointfromPotion)
                {
                    _healthControl.CurrentHealth = _healthControl.MaxHealth;
                }
                else
                {
                    _healthControl.CurrentHealth += _healPointfromPotion;
                }
                break;

            case (1):
                StopCoroutine(ExecuteAfterTime(0f));
                _speedPotionParticle.Play();
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

    private void CantDrinkAnyPotions()
    {
        CanDrinkAnyPotions = false;
        StartCoroutine(ExecuteAfterTime(1f));
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
