using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentExpText;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private ExperienceInfo _experienceInfo;
    [SerializeField] private LevelUpStats _levelUpStats;

    private void Start()
    {
        _textLevel.text = _currentLevel.ToString();
        _expSlider.maxValue = _experienceInfo.NeedExperienceForNextLevel[_currentLevel];
        UpdateText();
    }


    private void OnEnable()
    {
        CustomEvents.OnChangeExperience += ChangeExperience;
    }

    public void ChangeExperience(int exp)
    {
        if (exp >= _expSlider.maxValue - _expSlider.value)
        {
            var surplus = exp - (_expSlider.maxValue - _expSlider.value);
            NewLevel();
            _expSlider.value = surplus;
        }
        else
        {
            _expSlider.value += exp;
        }
        UpdateText();
    }

    private void NewLevel()
    {
        _currentLevel++;
        _textLevel.text = _currentLevel.ToString();
        _expSlider.maxValue = _experienceInfo.NeedExperienceForNextLevel[_currentLevel];
        _levelUpStats.NewLevel();
    }

    private void UpdateText()
    {
        _currentExpText.text = _expSlider.value.ToString() + " / " + _experienceInfo.NeedExperienceForNextLevel[_currentLevel].ToString();
    }

    private void OnDisable()
    {
        CustomEvents.OnChangeExperience -= ChangeExperience;
    }
}
