using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private StylizedWater2.UnderwaterRenderer _renderMat;
    [SerializeField] private ParticleSystem[] _waterParticle;

    [SerializeField] private Material[] _underWaterMaterials;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            _renderMat.waterMaterial = _underWaterMaterials[1];
            _renderMat.UpdateMaterialParameters();
            ParticleToggle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            _renderMat.waterMaterial = _underWaterMaterials[0];
            _renderMat.UpdateMaterialParameters();
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
