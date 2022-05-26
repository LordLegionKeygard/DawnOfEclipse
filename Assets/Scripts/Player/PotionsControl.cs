using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsControl : MonoBehaviour
{
    public static bool CanDrinkAnyPotions = true;
    [SerializeField] private ParticleSystem _speedPotionParticle;
    [SerializeField] private int _healPointFromPotion;
    [SerializeField] private int _manaPointFromPotion;
    [SerializeField] private HealthControl _healthControl;
    [SerializeField] private ManaControl _manaControl;
    [SerializeField] private PlayerMovement _playerMovement;
    public float PotionSpeed;
    public bool SpeedPotion = false;

    private void OnEnable()
    {
        CustomEvents.OnUsePotion += UsePotions;
    }

    public void CalculatePotions()
    {
        _healPointFromPotion = _healthControl.MaxHealth / 3;
        _manaPointFromPotion = _manaControl.MaxMana / 3;
    }
    private void UsePotions(int potion)
    {
        if (!CanDrinkAnyPotions) return;
        CantDrinkAnyPotions();

        switch (potion)
        {
            case (0):
                if (_healthControl.CurrentHealth > _healthControl.MaxHealth - _healPointFromPotion)
                {
                    _healthControl.CurrentHealth = _healthControl.MaxHealth;
                    _healthControl.UpdateSlider();
                }
                else
                {
                    _healthControl.CurrentHealth += _healPointFromPotion;
                    _healthControl.UpdateSlider();
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
                if (_manaControl.CurrentMana > _manaControl.MaxMana - _manaPointFromPotion)
                {
                    _manaControl.CurrentMana = _manaControl.MaxMana;
                    _manaControl.UpdateSlider();
                }
                else
                {
                    _manaControl.CurrentMana += _manaPointFromPotion;
                    _manaControl.UpdateSlider();
                }
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
