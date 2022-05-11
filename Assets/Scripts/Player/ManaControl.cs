using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControl : MonoBehaviour
{
    [SerializeField] private Slider _manaBar;
    public int MaxMana;
    private float _currentMana;
    [SerializeField] private float _regenNumber;
    public float CurrentMana => _currentMana;

    public void CalculateMana()
    {
        _currentMana = MaxMana;
        _manaBar.maxValue = MaxMana;
        _manaBar.value = MaxMana;
    }

    private void Update()
    {
        if(_currentMana < MaxMana)
        {
            _currentMana += _regenNumber;
            _manaBar.value = _currentMana;
        }
    }

    public void UseMana(int amount)
    {
        if (_currentMana - amount >= 0)
        {
            _currentMana -= amount;
            _manaBar.value = _currentMana;
        }
    }
}
