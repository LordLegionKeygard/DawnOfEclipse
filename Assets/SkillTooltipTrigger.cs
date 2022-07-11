using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TooltipSystem _tooltipSystem;

    [SerializeField] private SkillInfo _skillInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipSystem.ShowToggle(true, 1);
        CustomEvents.FireUpdateSkillInfoText(_skillInfo.SkillName,
                                             _skillInfo.SkillInformation,
                                             "Cost: " + _skillInfo.ManaCost.ToString() +" mana", 
                                             "Cooldown: " + _skillInfo.Cooldown.ToString() + " sec");

        CustomEvents.FireUpdateSkillToolTipTransform(transform.position.x, transform.position.y);
        float puk = transform.position.x;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipSystem.ShowToggle(false, 1);
    }
}
