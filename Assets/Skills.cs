using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _manaCostText;
    [SerializeField] private ManaControl _manaControl;
    public int ManaCost;
    public KeyCode _keyCode;
    public Transform TargetTransform;
    public AudioSource SkillSound;
    public Image SkillImage;
    public Image BackSkillImage;
    public float DefaultSkillTime;
    public float Cooldown;
    public bool SkillToggle;
    private float _timeFillAmount;
    private float _skillTime;
    public SkillInfo SkillInfo;

    private void Start()
    {
        if(SkillInfo == null) return;
        _skillTime = SkillInfo.DefaultSkillTime;
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
        if(ManaCost > _manaControl.CurrentMana || HealthControl.IsDeath) return;
        if (SkillImage.fillAmount == 1)
        {
            DoSkill(true);
            _manaControl.UseMana(ManaCost);
            SkillToggle = true;
            SkillImage.fillAmount = 0;
        }
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            SkillTurnOn();
        }
        if (SkillImage.fillAmount != 1) SkillImage.fillAmount += Time.deltaTime / _timeFillAmount;

        if (SkillToggle)
        {
            _skillTime -= Time.deltaTime;
            if (_skillTime <= 0)
            {
                DoSkill(false);
                _skillTime = DefaultSkillTime;
                SkillToggle = false;
            }
        }
    }

    public virtual void DoSkill(bool toggle)
    {

    }
}