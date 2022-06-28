using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsControl : MonoBehaviour
{
    public static bool CanDrinkAnyPotions = true;
    [SerializeField] private ParticleSystem _speedPotionParticle;
    [SerializeField] private int _healPointFromPotion;
    [SerializeField] private int _manaPointFromPotion;
    [SerializeField] private HealthControl _healthControl;
    [SerializeField] private ManaControl _manaControl;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BuffIconSpawner _buffIconSpawner;
    [SerializeField] private Sprite[] _potionsImage;
    [SerializeField] private GameObject[] _potionsVFX;

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

        var potVFX = Instantiate(_potionsVFX[potion], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.4f, this.gameObject.transform.position.z), Quaternion.identity);
        potVFX.transform.SetParent(this.gameObject.transform);

        switch (potion)
        {
            case (0): //health
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
            case (1): //speed
                StopCoroutine(ExecuteAfterTime(0f));
                _buffIconSpawner.SpawnBuffIcon(_potionsImage[potion], 20, 16);
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
            case (2): //mana
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
            case (3): //mead
                CustomEvents.FireStatBuff(0, 5);
                _buffIconSpawner.SpawnBuffIcon(_potionsImage[potion], 60, 17);
                break;
            case (4): //wine
                CustomEvents.FireStatBuff(1, 5);
                _buffIconSpawner.SpawnBuffIcon(_potionsImage[potion], 60, 18);
                break;
            case (5): //ale
                CustomEvents.FireStatBuff(2, 5);
                _buffIconSpawner.SpawnBuffIcon(_potionsImage[potion], 60, 19);
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
