using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceInfo", menuName = "Info/Experience")]
public class ExperienceInfo : ScriptableObject
{
    [SerializeField] public int[] NeedExperienceForNextLevel;
}
