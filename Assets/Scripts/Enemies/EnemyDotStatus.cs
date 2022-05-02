using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDotStatus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dotVisualPs;
    [SerializeField] private int _poison;
    private EnemyStats _enemyStats;

    private bool isPoison;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void TakePosionDamage(int poisonPoints)
    {
        if (isPoison) return;
        _poison += poisonPoints;
        if (_poison >= 100) { DotActive(); }
    }

    private void DotActive()
    {
        isPoison = true;
        _dotVisualPs.Play();
        DotUpdate();
    }

    private void DotUpdate()
    {
        _enemyStats.CurrentHealth -= 5;
        _enemyStats.UpdateSlider();
        Invoke("DotUpdate", 1f);
    }
}
