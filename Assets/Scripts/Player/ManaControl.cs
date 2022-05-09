using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControl : MonoBehaviour
{
    [SerializeField] private Slider _manaBar;
    private int _maxMana = 100;
    private int _currentMana;
    private WaitForSeconds _regenTick = new WaitForSeconds(2f);
    private Coroutine _regen;
    public int CurrentMana => _currentMana;
    [HideInInspector] public bool ManaRun = false;

    private void Start()
    {
        _currentMana = _maxMana;
        _manaBar.maxValue = _maxMana;
        _manaBar.value = _maxMana;
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

        while (_currentMana < _maxMana)
        {
            _currentMana += _maxMana / 1000;
            _manaBar.value = _currentMana;
            yield return _regenTick;
        }
        _regen = null;
    }
}
