using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomePickUp : MonoBehaviour
{
    [SerializeField] private SkillTomes _skillTomes;
    [SerializeField] private Animator _animator;
    private bool _canTake = true;

    public void PickUp()
    {
        if (!_canTake) return;

        _canTake = false;
        CustomEvents.FireTakeTome((int)_skillTomes);
        _animator.SetTrigger("take");

        Destroy(gameObject, 3f);
    }
}
