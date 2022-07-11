using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] private SkillTomes _skillTomes;
    [SerializeField] private bool _isLearn;
    public bool _isOpen;

    [SerializeField] private SkillTreeButton _skillTreeButton;

    [Header("Icon")]
    [SerializeField] private Image[] _skillFrameAndArrows;
    [SerializeField] private Image[] _nextSkillFrame;
    [SerializeField] private Image[] _nextSkillIcon;

    public void CheckSkill()
    {
        Debug.Log("Click");
        if (_isLearn || !_isOpen) return;

        if (CharacterSkillTreePoints.CharacterSkillTreePointsS.HavePoints((int)_skillTomes))
        {
            _isLearn = true;
            CustomEvents.FireChangeSkillTreePoints((int)_skillTomes, -1);
            LearnSkill();
        }
    }

    private void LearnSkill()
    {
        Debug.Log("Learn skill :" + _skillTomes.ToString() + "magic school");
        foreach (var item in _skillFrameAndArrows) item.color = new Color(1, 0.7843137f, 0, 1);

        foreach (var item in _nextSkillFrame) item.color = new Color(0, 1, 0.06542563f, 1);

        foreach (var item in _nextSkillIcon) item.color = new Color(1, 1, 1, 1);

        _skillTreeButton._isOpen = true;
    }
}
