using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSkills : MonoBehaviour
{
    private PlayerAnimatorManager _playerAnimatorManager;
    private bool _canUseSkill = true;
    [SerializeField] private ParticleSystem[] _mushroomParticles; 
    [SerializeField] private float _skillCooldown;
    [SerializeField] private float _skillCooldownConstant;

    private void Awake()
    {
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }
    private void OnEnable()
    {
        CustomEvents.OnUseRaceSkill += UseRaceSkill;
    }

    private void Update()
    {
        if(!_canUseSkill)
        {
            _skillCooldown -= Time.deltaTime;
            if(_skillCooldown <= 0)
            {
                _canUseSkill = true;
                _skillCooldown = _skillCooldownConstant;
            }
        }
    }

    private void UseRaceSkill()
    {
        if (!_canUseSkill) return;

        _canUseSkill = false;
        _playerAnimatorManager.AnimatorRaceSkillTrigger();
        switch (CharacterInformation.Race)
        {
            case 0: //satyr
                break;
            case 1: // mushroom
                _mushroomParticles[0].Play();
                _mushroomParticles[1].Play();
                Instantiate(_mushroomParticles[2], new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
                StartCoroutine(ExecuteAfterTime(0.5f));
                IEnumerator ExecuteAfterTime(float timeInSec)
                {
                    yield return new WaitForSeconds(timeInSec);
                    _mushroomParticles[0].Stop();
                    _mushroomParticles[1].Stop();
                }
                break;
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnUseRaceSkill -= UseRaceSkill;
    }
}
