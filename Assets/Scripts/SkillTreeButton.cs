using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] private SkillTomes _skillTomes;
    [SerializeField] private SkillTreeButton[] _nextSkillTreeButton;
    [SerializeField] private Image[] _arrows;
    [SerializeField] private Image _skillFrame;
    private Image _skillIcon;
    public bool CanLearn;
    public bool IsLearn;

    private void Start()
    {
        _skillIcon = GetComponent<Image>();
    }

    public void CheckSkill()
    {
        if (IsLearn || !CanLearn) return;

        if (CharacterSkillTreePoints.CharacterSkillTreePointsS.HavePoints((int)_skillTomes))
        {
            IsLearn = true;
            CustomEvents.FireChangeSkillTreePoints((int)_skillTomes, -1);
            LearnSkill();
        }
    }

    public void CanLearnSkill()
    {
        if (CanLearn) return;
        CanLearn = true;
        _skillFrame.color = new Color(0, 1, 0.06542563f, 1);
        _skillIcon.color = new Color(1, 1, 1, 1);
    }

    private void LearnSkill()
    {
        Debug.Log("Learn skill: " + _skillTomes.ToString() + " Dark Tree");
        IsLearn = true;
        _skillFrame.color = new Color(1, 0.7843137f, 0, 1);

        if (_nextSkillTreeButton[0] != null)
        {
            foreach (var item in _arrows) item.color = new Color(1, 0.7843137f, 0, 1);
            foreach (var item in _nextSkillTreeButton) item.CanLearnSkill();
        }
    }
}
