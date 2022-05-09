using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainStats;
    [SerializeField] private HealthControl _healthControl;
    [SerializeField] private ManaControl _manaControl;
    [SerializeField] private StaminaControl _staminaControl;

    private void OnEnable()
    {
        CustomEvents.OnUpdateAllStats += UpdateAllMainStats;
    }

    private void UpdateAllMainStats()
    {
        _mainStats[0].text = _healthControl.MaxHealth.ToString();
        _mainStats[1].text = _manaControl.MaxMana.ToString();
        _mainStats[2].text = _staminaControl.MaxStamina.ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnUpdateAllStats -= UpdateAllMainStats;
    }
}
