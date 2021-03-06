using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Info/Skill")]
public class SkillInfo : ScriptableObject
{
    public int ManaCost;
    public string SkillName;
    public AudioSource SkillSound;
    public Sprite SkillIcon;
    public float Cooldown;
    public int AnimationNumber;
    public int CastPointNumber;
    public int SkillPointNumber;
    public float TimeToCastSkill;
    public bool NotRotate;
    public bool IsBuff;
    public float BuffCooldown;
    public int BuffNumber;
    public string SkillInformation;

    [Header ("Prefabs")]

    public GameObject SkillCastPrefab;
    public GameObject SkillPrefab;
}
