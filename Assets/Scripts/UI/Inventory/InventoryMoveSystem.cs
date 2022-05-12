using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMoveSystem : MonoBehaviour
{
    [SerializeField] private TransformGetSiblingIndex _transformGetSiblingIndex;
    [SerializeField] private GameObject[] _panels;
    private int _number = 0;
    private bool _canMove;
    private void FixedUpdate()
    {
        if (_panels[_number].transform.position.x > 1800)
        {
            _panels[_number].transform.position = new Vector3(1799, _panels[_number].transform.position.y, _panels[_number].transform.position.z);           
        }
        if (_panels[_number].transform.position.x < 100)
        {
            _panels[_number].transform.position = new Vector3(101, _panels[_number].transform.position.y, _panels[_number].transform.position.z);
        }
        if (_panels[_number].transform.position.y > 1050)
        {
            _panels[_number].transform.position = new Vector3(_panels[_number].transform.position.x, 1049, _panels[_number].transform.position.z);
        }
        if (_panels[_number].transform.position.y < 20)
        {
            _panels[_number].transform.position = new Vector3(_panels[_number].transform.position.x, 21, _panels[_number].transform.position.z);
        }
        if (!_canMove) return;

        _panels[_number].transform.position = Input.mousePosition;
    }

    public void MoveToggle(bool state)
    {
        _canMove = (state);
    }

    public void ChangePanelsNumber(int number)
    {
        _number = number;
        _canMove = true;
        _transformGetSiblingIndex.ChangeSiblingIndex(number);
    }
}
