using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] private Slider _staminaBar;
    private int _maxStamina = 1000;
    private int _currentStamina;
    private WaitForSeconds _regenTick = new WaitForSeconds(0.0002f);
    private Coroutine _regen;
    public int CurrentStamina => _currentStamina;
    [HideInInspector] public bool StaminaRun = false;

    private void Start()
    {
        _currentStamina = _maxStamina;
        _staminaBar.maxValue = _maxStamina;
        _staminaBar.value = _maxStamina;
    }

    private void FixedUpdate()
    {
        if (_currentStamina > 10 && StaminaRun)
        {
            _currentStamina -= 2;
            _staminaBar.value = _currentStamina;
            RegenTimer();
        }
    }

    private void Update()
    {
        if (_staminaBar.value == _currentStamina) { return; }
        if (_staminaBar.value > _currentStamina)
        {
            _staminaBar.value -= Time.deltaTime * 400;
        }
        if (_staminaBar.value < _currentStamina)
        {
            _staminaBar.value += Time.deltaTime * 400;
        }

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
        if (_regen != null)
            StopCoroutine(_regen);

        _regen = StartCoroutine(RegenStamina());
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1f);

        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _maxStamina / 1000;
            _staminaBar.value = _currentStamina;
            yield return _regenTick;
        }
        _regen = null;
    }
}
