using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    PlayerController playerController;
    public float currentWeaponDamage;

    private BoxCollider col;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            HealthControl playerHealth = collision.GetComponent<HealthControl>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currentWeaponDamage);
                col.enabled = false;
            }
        }
    }
}
