using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    private EnemyLevel _enemyLevel;

    private void Awake()
    {
        _enemyLevel = GetComponent<EnemyLevel>();
    }

    public void CalculateLoot()
    {
        CalculateMoons();
        CalculateDropItem();
    }

    private void CalculateDropItem()
    {

    }

    private void CalculateMoons()
    {
        var rnd = Random.Range(_enemyLevel.EnemyInformation.MinMoons[_enemyLevel.Level], _enemyLevel.EnemyInformation.MaxMoons[_enemyLevel.Level]);
        CustomEvents.FireChangeCoins(rnd);
        Debug.Log("Enemy drop - " + rnd + " moons");
    }
}
