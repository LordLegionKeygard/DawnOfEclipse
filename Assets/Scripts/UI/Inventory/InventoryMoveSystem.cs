using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMoveSystem : MonoBehaviour
{
    [SerializeField] private TransformGetSiblingIndex _transformGetSiblingIndex;
    [SerializeField] private GameObject[] _panels;
    private int _number;
    private bool _canMove;
    private void Update()
    {
        if (!_canMove) return;

        _panels[_number].transform.position = Input.mousePosition;
    }

    public void MoveToggle(bool state)
    {
        _canMove = (state);
        CheckTransform();
    }

    private void CheckTransform()
    {
        var trans = _panels[_number].transform.position;
        if (trans.x > Screen.width)
        {
            _panels[_number].transform.position = new Vector3(Screen.width - 100, trans.y, trans.z);
        }
        else if (trans.x < 100)
        {
            _panels[_number].transform.position = new Vector3(100, trans.y, trans.z);
        }
        else if (trans.y > Screen.height)
        {
            _panels[_number].transform.position = new Vector3(trans.x, Screen.height - 100, trans.z);
        }
        else if (trans.y < 20)
        {
            _panels[_number].transform.position = new Vector3(trans.x, 100, trans.z);
        }
    }

    public void ChangePanelsNumber(int number)
    {
        _number = number;
        _canMove = true;
        _transformGetSiblingIndex.ChangeSiblingIndex(number);
    }
}
