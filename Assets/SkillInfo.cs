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
    public float DefaultSkillTime;
    public float Cooldown;
}
