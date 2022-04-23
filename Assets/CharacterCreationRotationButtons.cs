using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationRotationButtons : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private float _rotateNumber = 190;
    private bool _right;
    private bool _left;

    private void Update()
    {
        _character.transform.rotation = Quaternion.Euler(_character.transform.rotation.x, _rotateNumber, _character.transform.rotation.z);
        if (_right)
        {
            _rotateNumber += 1;
        }
        if (_left)
        {
            _rotateNumber -= 1;
        }
    }

    public void Rotate(int number)
    {
        if (number == 0)
            _right = true;
        else
            _left = true;
    }

    public void UnRotate()
    {
        _right = false;
        _left = false;
    }
}
