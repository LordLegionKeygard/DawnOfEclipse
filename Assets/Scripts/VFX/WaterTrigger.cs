using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _waterParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            ParticleToggle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            ParticleToggle(false);          
        }
    }

    private void ParticleToggle(bool state)
    {
        CustomEvents.FirePlayerInWaterVFX(state);
        foreach (var item in _waterParticle)
        {
            if (state) item.Play();
            else item.Stop();
        }
    }
}
