using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    PlayerController playerController;
    public int currentWeaponDamage;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            HealthControl playerHealth = collision.GetComponent<HealthControl>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
