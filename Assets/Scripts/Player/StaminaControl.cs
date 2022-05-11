using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _staminaBarBack;
    public int MaxStamina;
    [SerializeField] private float _currentStamina;
    private WaitForSeconds _regenTick = new WaitForSeconds(0.002f);
    [SerializeField] private float _regenNumber;
    private Coroutine _regen;
    public float CurrentStamina => _currentStamina;
    [HideInInspector] public bool StaminaRun = false;

    public void CalculateStamina()
    {
        _currentStamina = MaxStamina;
        _staminaBar.maxValue = MaxStamina;
        _staminaBar.value = MaxStamina;

        _staminaBarBack.maxValue = MaxStamina;
        _staminaBarBack.value = MaxStamina;
    }

    private void Update()
    {
        if (_currentStamina > 0 && StaminaRun)
        {
            _currentStamina -= 0.2f;
            _staminaBar.value = _currentStamina;
            _staminaBarBack.value = _currentStamina;
            RegenTimer();
        }
    }

    private void UpdateBackSlider()
    {
        _staminaBarBack.DOValue(_currentStamina, 1f,false);
    }

    public void UseStamina(int amount)
    {
        if (_currentStamina - amount >= 0)
        {        
            _currentStamina -= amount;
            UpdateBackSlider();
            _staminaBar.value = _currentStamina;
            RegenTimer();
        }
    }

    private void RegenTimer()
    {
        if (_regen != null) StopCoroutine(_regen);
        _regen = StartCoroutine(RegenStamina());
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1f);

        while (_currentStamina < MaxStamina)
        {
            _currentStamina += _regenNumber;
            _staminaBar.value = _currentStamina;
            _staminaBarBack.value = _currentStamina;
            yield return _regenTick;
        }
        _regen = null;
    }
}
