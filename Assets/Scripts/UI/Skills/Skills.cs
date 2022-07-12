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
        SkillManaCost.ManaCost = SkillInfo.ManaCost;
    }

    private void CanUseSkillToggle(bool state)
    {
        IsCanUseSkill = state;
    }

    private void Start()
    {
        if (SkillInfo == null) return;
        _timeFillAmount = SkillInfo.Cooldown;
        ManaCost = SkillInfo.ManaCost;
        SkillImage.sprite = SkillInfo.SkillIcon;
        BackSkillImage.sprite = SkillInfo.SkillIcon;
        _manaCostText.text = ManaCost.ToString();
        Active();
    }

    private void Active()
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
        if (Input.GetKeyDown(_keyCode))
        {
            SkillTurnOn();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CustomEvents.FireActiveTargetSkill(false);
        }
        if (SkillImage.fillAmount != 1) SkillImage.fillAmount += Time.deltaTime / _timeFillAmount;
    }

    public virtual void DoSkill()
    {

    }

    private void OnDisable()
    {
        CustomEvents.OnCanUseSkill -= CanUseSkillToggle;
    }
}