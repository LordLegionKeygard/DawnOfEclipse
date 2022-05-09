using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstitutionModifier", menuName = "Info/ConstitutionModifier")]
public class ConstitutionModifier : ScriptableObject
{
    [SerializeField] public float[] ConModifier;
}
