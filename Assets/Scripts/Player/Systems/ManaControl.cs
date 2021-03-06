using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControl : MonoBehaviour
{
    [SerializeField] private Slider _manaBar;
    public int MaxMana;
    public float CurrentMana;
    [SerializeField] private float _regenNumber;

    private void OnEnable()
    {
        CustomEvents.OnUseMana += ChangeMana;
        CustomEvents.OnReturnPlayerStats += ReturnStatAfterBuff;
    }

    public void CalculateMana(bool isBuff)
    {
        _manaBar.maxValue = MaxMana;
        _manaBar.value = MaxMana;

        if (!isBuff) CurrentMana = MaxMana;
    }

    private void Update()
    {
        if (CurrentMana < MaxMana)
        {
            CurrentMana += _regenNumber;
            _manaBar.value = CurrentMana;
        }
    }

    private void ChangeMana(int amount)
    {
        if (CurrentMana - amount >= 0)
        {
            CurrentMana -= amount;
        }
    }

    public void UpdateSlider()
    {
        _manaBar.value = CurrentMana;
    }

    private void ReturnStatAfterBuff(int statNumber)
    {
        if (statNumber == 6)
        {
            CurrentMana = MaxMana;
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnUseMana -= ChangeMana;
        CustomEvents.OnReturnPlayerStats -= ReturnStatAfterBuff;
    }
}
