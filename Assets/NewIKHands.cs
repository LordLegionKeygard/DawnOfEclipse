using System.Collections;
using UnityEngine;


public class NewIKHands : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [Range(0, 1)][SerializeField] private float _leftHandPositionWeight;
    [Range(0, 1)][SerializeField] private float _leftHandRotationWeight;
    [Range(0, 1)][SerializeField] private float _rightHandPositionWeight;
    [Range(0, 1)][SerializeField] private float _rightHandRotationWeight;

    private void OnEnable()
    {
        CustomEvents.OnChangeIKHands += ChangeIK;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void ChangeIK(int number)
    {
        switch (number)
        {
            case 0: //Unarmed
                _leftHandPositionWeight = 0;
                _leftHandRotationWeight = 0;
                _rightHandPositionWeight = 0;
                _rightHandRotationWeight = 0;
                break;
            case 1: //GreatSword
                _leftHandPositionWeight = 1;
                _leftHandRotationWeight = 1;
                _rightHandPositionWeight = 1;
                _rightHandRotationWeight = 1;
                break;
        }
    }

    private void OnAnimatorIK()
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandPositionWeight);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftHandRotationWeight);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandPositionWeight);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightHandRotationWeight);
    }

    private void OnDisable()
    {
        CustomEvents.OnChangeIKHands -= ChangeIK;
    }
}
