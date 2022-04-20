using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _takeDamagePS; 
    
    public void TakeDamageVFX()
    {
        _takeDamagePS.Play();
    }
}
