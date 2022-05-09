using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControl : MonoBehaviour
{
    [SerializeField] private Slider _manaBar;
    public int MaxMana;
    private int _currentMana;
    private WaitForSeconds _regenTick = new WaitForSeconds(2f);
    private Coroutine _regen;
    public int CurrentMana => _currentMana;
    [HideInInspector] public bool ManaRun = false;

    public void CalculateMana()
    {
        _currentMana = MaxMana;
        _manaBar.maxValue = MaxMana;
        _manaBar.value = MaxMana;
    }

    private void FixedUpdate()
    {
        if (_currentMana > 10 && ManaRun)
        {
            _currentMana -= 2;
            _manaBar.value = _currentMana;
            RegenTimer();
        }
    }

    private void Update()
    {
        if (_manaBar.value == _currentMana) { return; }
        if (_manaBar.value > _currentMana)
        {
            _manaBar.value -= Time.deltaTime * 4;
        }
        if (_manaBar.value < _currentMana)
        {
            _manaBar.value += Time.deltaTime * 4;
        }

    }

    public void UseMana(int amount)
    {
        if (_currentMana - amount >= 0)
        {
            _currentMana -= amount;
            RegenTimer();
        }
    }

    private void RegenTimer()
    {
        if (_regen != null)
            StopCoroutine(_regen);

        _regen = StartCoroutine(RegenMana());
    }

    private IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(1f);

        while (_currentMana < MaxMana)
        {
            _currentMana += MaxMana / 1000;
            _manaBar.value = _currentMana;
            yield return _regenTick;
        }
        _regen = null;
    }
}
