using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    PlayerController playerController;
    private Collider damageCollider;

    public int currentWeaponDamage;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if(playerController.attack)
        {
            EnableDamageCollider();
        }
        else
        {
            DisableDamageCollider();
        }
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
