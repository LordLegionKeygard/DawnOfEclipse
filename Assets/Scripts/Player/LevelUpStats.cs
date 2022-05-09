using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpStats : MonoBehaviour
{
    [SerializeField] private int _strength;
    [SerializeField] private int _dexterity;
    [SerializeField] private int _constitution;
    [SerializeField] private int _vigor;
    [SerializeField] private int _intelligence;
    [SerializeField] private int _wisdom;
    [SerializeField] private int _mind;
    public int SkillPoint;
    [SerializeField] private int[] _stats;
    [SerializeField] private TextMeshProUGUI[] _statsText;
    [SerializeField] private TextMeshProUGUI _usablePointText;
    [SerializeField] private Button[] _statPlusButtons;
    [SerializeField] private Button[] _statMinusButtons;

    private void Start()
    {
        NewLevel();
        NewLevel();
    }

    public void AddStat(int serialNumber)
    {
        _statMinusButtons[serialNumber].interactable = true;
        _stats[serialNumber]++;
        SkillPoint--;
        CheckEachButtons();
        UpdateText(serialNumber);
    }

    public void NewLevel()
    {
        SkillPoint++;
        _usablePointText.text = SkillPoint.ToString();
        foreach (var item in _statPlusButtons) { item.interactable = true; }
    }

    public void ReduceStat(int serialNumber)
    {
        foreach (var item in _statPlusButtons) item.interactable = true;
        _stats[serialNumber]--;
        SkillPoint++;
        if(_stats[serialNumber] == 0)
        {
            _statMinusButtons[serialNumber].interactable = false;
        }
        CheckEachButtons();
        UpdateText(serialNumber);
    }

    private void CheckEachButtons()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            if (SkillPoint > 0)
            {
                _statPlusButtons[i].interactable = true;
            }
            else
            {
                _statPlusButtons[i].interactable = false;
            }
        }
    }

    private void UpdateText(int serialNumber)
    {
        _statsText[serialNumber].text = _stats[serialNumber].ToString();
        _usablePointText.text = SkillPoint.ToString();
    }
}
