using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAoeSkill : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    [SerializeField] private float _activateTime;

    [SerializeField] private float _lifeTime;

    private void Start()
    {
        Invoke("CollActive", _activateTime);
        Invoke("Death", _lifeTime);
    }

    private void CollActive()
    {
        _collider.enabled = true;
    }

    private void Death()
    {
        _collider.enabled = false;
        Destroy(gameObject,2f);
    }
}
