using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] private Slider _staminaBar;
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
    }

    private void FixedUpdate()
    {
        if (_currentStamina > 0 && StaminaRun)
        {
            _currentStamina -= 0.2f;
            _staminaBar.value = _currentStamina;
            RegenTimer();
        }
    }

    private void Update()
    {
        if (_staminaBar.value == _currentStamina) return;
        if (_staminaBar.value > _currentStamina) _staminaBar.value -= Time.deltaTime * 50;
        if (_staminaBar.value < _currentStamina) _staminaBar.value += Time.deltaTime * 50;
    }

    public void UseStamina(int amount)
    {
        if (_currentStamina - amount >= 0)
        {
            _currentStamina -= amount;
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
            yield return _regenTick;
        }
        _regen = null;
    }
}
