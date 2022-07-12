using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills : MonoBehaviour
{
    public bool IsCooldownAfterUse;
    [SerializeField] private TextMeshProUGUI _manaCostText;
    public ManaControl ManaControl;
    public int ManaCost;
    public KeyCode _keyCode;
    public AudioSource SkillSound;
    public Image SkillImage;
    public Image BackSkillImage;
    private float _timeFillAmount;
    public SkillInfo SkillInfo;
    public bool IsCanUseSkill = true;
    public SkillManaCost SkillManaCost;

    private void OnEnable()
    {
        CustomEvents.OnCanUseSkill += CanUseSkillToggle;
        CustomEvents.OnUpdateSkillPanels += UpdateSkill;
    }

    private void CanUseSkillToggle(bool state)
    {
        IsCanUseSkill = state;
    }

    private void UpdateSkill()
    {
        if (SkillInfo == null) return;
        SkillManaCost.ManaCost = SkillInfo.ManaCost;
        _timeFillAmount = SkillInfo.Cooldown;
        ManaCost = SkillInfo.ManaCost;
        SkillImage.sprite = SkillInfo.SkillIcon;
        BackSkillImage.sprite = SkillInfo.SkillIcon;
        _manaCostText.text = ManaCost.ToString();
        ActiveUI();
    }

    private void ActiveUI()
    {
        _manaCostText.enabled = true;
        SkillImage.enabled = true;
        BackSkillImage.enabled = true;
    }
    public virtual void SkillTurnOn()
    {
        if (ManaCost > ManaControl.CurrentMana || HealthControl.IsDeath || !IsCanUseSkill) return;
        if (SkillImage.fillAmount == 1)
        {
            DoSkill();
            if (IsCooldownAfterUse) return;
            CustomEvents.FireUseMana(ManaCost);
            SkillImage.fillAmount = 0;
        }
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(_keyCode)) SkillTurnOn();

        if (SkillImage.fillAmount != 1) SkillImage.fillAmount += Time.deltaTime / _timeFillAmount;
    }

    public virtual void DoSkill()
    {

    }

    private void OnDisable()
    {
        CustomEvents.OnCanUseSkill -= CanUseSkillToggle;
        CustomEvents.OnUpdateSkillPanels -= UpdateSkill;
    }
}