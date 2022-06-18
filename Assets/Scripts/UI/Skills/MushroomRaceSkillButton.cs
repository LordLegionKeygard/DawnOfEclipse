using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomRaceSkillButton : Skills
{
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;
    [SerializeField] private ParticleSystem[] _mushroomParticles;

    public override void DoSkill(bool state)
    {
        if (state)
        {
            _playerAnimatorManager.AnimatorSkillTrigger("Skills", 1);

            _mushroomParticles[0].Play();
            _mushroomParticles[1].Play();
            Instantiate(_mushroomParticles[2], new Vector3(TargetTransform.transform.position.x, TargetTransform.transform.position.y + 0.3f, TargetTransform.transform.position.z), Quaternion.identity);
            StartCoroutine(ExecuteAfterTime(0.5f));
            IEnumerator ExecuteAfterTime(float timeInSec)
            {
                yield return new WaitForSeconds(timeInSec);
                _mushroomParticles[0].Stop();
                _mushroomParticles[1].Stop();
            }
            Invoke("ResetAnimation", 0.5f);
        }
    }

    private void ResetAnimation()
    {
        _playerAnimatorManager.AnimatorSkillTrigger("Skills", 0);
    }
}
