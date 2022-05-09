using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemyInformation", menuName = "Info/Enemy")]
public class EnemyInformation : ScriptableObject
{
    [SerializeField] public int[] Exp;
    [SerializeField] public int[] Health;
    [SerializeField] public int[] MinMoons, MaxMoons;
    [SerializeField] public int[] physAttack;
    [SerializeField] public int[] magAttack;
    [SerializeField] public int[] physDef;
    [SerializeField] public int[] magDef;
}
