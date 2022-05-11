using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpStats : MonoBehaviour
{
    //  _strength;
    //  _dexterity;
    //  _constitution;
    //  _vigor;
    //  _intelligence;
    //  _wisdom;
    //  _mind;
    public int SkillPoint;
    public int[] Stats;
    [SerializeField] private TextMeshProUGUI[] _statsText;
    [SerializeField] private TextMeshProUGUI _usablePointText;
    [SerializeField] private Button[] _statPlusButtons;
    [SerializeField] private Button[] _statMinusButtons;
    [SerializeField] private Button _acceptButton;

    private void OnEnable()
    {
        CustomEvents.OnUpdateAllStats += UpdateAllStats;
    }

    public void AddStat(int serialNumber)
    {
        _statMinusButtons[serialNumber].interactable = true;
        Stats[serialNumber]++;
        SkillPoint--;
        CheckEachButtons();
        CheckAcceptButton();
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
        Stats[serialNumber]--;
        SkillPoint++;
        if (Stats[serialNumber] == 0)
        {
            _statMinusButtons[serialNumber].interactable = false;
        }
        CheckEachButtons();
        CheckAcceptButton();
        UpdateText(serialNumber);
    }

    private void CheckEachButtons()
    {
        for (int i = 0; i < Stats.Length; i++)
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

    private void CheckAcceptButton()
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] != 0)
            {
                _acceptButton.interactable = true;
                return;
            }
            else
            {
                _acceptButton.interactable = false;
            }
        }
    }

    private void UpdateText(int serialNumber)
    {
        switch (serialNumber)
        {
            case 0:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Strength).ToString();
                break;
            case 1:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Dexterity).ToString();
                break;
            case 2:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Constitution).ToString();
                break;
            case 3:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Vigor).ToString();
                break;
            case 4:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Intelligence).ToString();
                break;
            case 5:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Wisdom).ToString();
                break;
            case 6:
                _statsText[serialNumber].text = (Stats[serialNumber] + CharacterStats.Mind).ToString();
                break;
        }
        _usablePointText.text = SkillPoint.ToString();
    }

    public void Accept()
    {
        CharacterStats.Strength += Stats[0];
        CharacterStats.Dexterity += Stats[1];
        CharacterStats.Constitution += Stats[2];
        CharacterStats.Vigor += Stats[3];
        CharacterStats.Intelligence += Stats[4];
        CharacterStats.Wisdom += Stats[5];
        CharacterStats.Mind += Stats[6];


        CustomEvents.FireUpdateBaseWeaponDamage();
        Debug.Log("1");
        CustomEvents.FireCalculateAllStats();

        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = 0;
        }

        _acceptButton.interactable = false;

        foreach (var item in _statMinusButtons) { item.interactable = false; }

        if (SkillPoint == 0)
        {
            foreach (var item in _statPlusButtons) { item.interactable = false; }
        }
    }

    public void UpdateAllStats()
    {
        _statsText[0].text = CharacterStats.Strength.ToString();
        _statsText[1].text = CharacterStats.Dexterity.ToString();
        _statsText[2].text = CharacterStats.Constitution.ToString();
        _statsText[3].text = CharacterStats.Vigor.ToString();
        _statsText[4].text = CharacterStats.Intelligence.ToString();
        _statsText[5].text = CharacterStats.Wisdom.ToString();
        _statsText[6].text = CharacterStats.Mind.ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnUpdateAllStats -= UpdateAllStats;
    }
}
