using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationRotationButtons : MonoBehaviour
{
    [SerializeField] private CharacterCreationRaceChanger _characterCreationRaceChanger;
    [SerializeField] private GameObject[] _characters;
    [SerializeField] private float _rotateNumber = 190;
    private bool _right;
    private bool _left;

    private void Update()
    {
        _characters[_characterCreationRaceChanger.Race].transform.rotation = Quaternion.Euler(_characters[_characterCreationRaceChanger.Race].transform.rotation.x, _rotateNumber, _characters[_characterCreationRaceChanger.Race].transform.rotation.z);
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
