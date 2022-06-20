using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAura : Skills
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private ParticleSystem[] _mushroomParticles;
    [SerializeField] private Transform _targetTransform;

    public override void DoSkill()
    {
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 1);

        _mushroomParticles[0].Play();
        _mushroomParticles[1].Play();
        Instantiate(_mushroomParticles[2], new Vector3(_targetTransform.transform.position.x, _targetTransform.transform.position.y + 0.3f, _targetTransform.transform.position.z), Quaternion.identity);
        StartCoroutine(ExecuteAfterTime(0.5f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _mushroomParticles[0].Stop();
            _mushroomParticles[1].Stop();
        }
        Invoke("ResetAnimation", 0.5f);
    }

    private void ResetAnimation()
    {
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 0);
    }
}
