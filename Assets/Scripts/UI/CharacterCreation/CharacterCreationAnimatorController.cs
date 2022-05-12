using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CustomEvents.OnCharacterCreate += CharacterSit;
    }

    private void CharacterSit()
    {
        _animator.SetTrigger("sit");
    }

    private void OnDisable()
    {
        CustomEvents.OnCharacterCreate -= CharacterSit;
    }
}
