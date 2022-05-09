using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeEnemyNameColor : MonoBehaviour
{
    [SerializeField] private EnemyLevel _enemyLevel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Color[] _nameColors;

    private void OnEnable()
    {
        CustomEvents.OnUpdateEnemyNameColorText += UpdateNameTextColor;
    }
    private void Awake()
    {
        UpdateNameTextColor();
    }

    private void UpdateNameTextColor()
    {
        var difference = _enemyLevel.Level - ExperienceControl.CurrentLevel;

        if (difference >= 11)
        {
            _nameText.color = _nameColors[0]; //red
        }
        if (difference >= 6 && difference <= 10)
        {
            _nameText.color = _nameColors[1]; //orange 
        }
        if (difference >= 3 && difference <= 5)
        {
            _nameText.color = _nameColors[2];//yellow
        }
        if (difference >= -2 && difference <= 2)
        {
            _nameText.color = _nameColors[3]; //white
        }
        if (difference >= -5 && difference <= -3)
        {
            _nameText.color = _nameColors[4]; //green
        }
        if (difference >= -10 && difference <= -6)
        {
            _nameText.color = _nameColors[5]; //light blue
        }
        if (difference <= -11)
        {
            _nameText.color = _nameColors[6]; //blue
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnUpdateEnemyNameColorText -= UpdateNameTextColor;
    }
}
