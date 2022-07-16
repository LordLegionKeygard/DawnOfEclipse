using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSkillInfo : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI[] _skillText;

    private void Awake()
    {
        CustomEvents.OnUpdateSkillInfoText += UpdateSkillInfoText;
        CustomEvents.OnUpdateSkillToolTipTransform += UpdateTransform;
    }

    public void UpdateSkillInfoText(string skillName, string skillInfo, string manaCost, string cooldown)
    {
        _skillText[0].text = skillName;
        _skillText[1].text = skillInfo;
        _skillText[2].text = manaCost;
        _skillText[3].text = cooldown;
    }

    public void UpdateTransform(float X, float Y)
    {
        transform.position = new Vector2(X, Y);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateSkillInfoText -= UpdateSkillInfoText;
        CustomEvents.OnUpdateSkillToolTipTransform -= UpdateTransform;
    }
}
