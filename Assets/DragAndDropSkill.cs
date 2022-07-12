using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropSkill : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private SkillInfo _skillInfo;
    private SkillTreeButton _skillTreeButton;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _skillBarParent;
    public RectTransform RectTransform;

    private void Awake()
    {
        _skillTreeButton = GetComponent<SkillTreeButton>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_skillTreeButton.IsLearn) return;

        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_skillTreeButton.IsLearn) return;

        CustomEvents.FireTooltipToggle(false, 1);

        GetComponentInChildren<Image>().raycastTarget = false;

        transform.SetParent(_skillBarParent.transform.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_skillTreeButton.IsLearn) return;
        
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(_parent.transform);
        RectTransform.anchoredPosition = new Vector2(30, 30);

        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.GetComponent<AllSkill>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ChangeSkill(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.GetComponent<AllSkill>());
        }
    }

    private void ChangeSkill(AllSkill allSkill)
    {
        allSkill.SkillInfo = _skillInfo;
        CustomEvents.FireUpdateSkillPanels();
    }
}
