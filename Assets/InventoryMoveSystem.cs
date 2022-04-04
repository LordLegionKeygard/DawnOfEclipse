using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMoveSystem : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;

    private bool _canMove;
    private void Update()
    {
        if (_inventory.transform.position.x > 1800)
        {
            _inventory.transform.position = new Vector3(1799, transform.position.y + 220, transform.position.z);
        }
        else if (_inventory.transform.position.x < 100)
        {
            _inventory.transform.position = new Vector3(101, transform.position.y + 220, transform.position.z);
        }
        else if (_inventory.transform.position.y > 1050)
        {
            _inventory.transform.position = new Vector3(transform.position.x, 1049, transform.position.z);
        }
        else if (_inventory.transform.position.y < 20)
        {
            _inventory.transform.position = new Vector3(transform.position.x, 21, transform.position.z);
        }
        else if (!_canMove) return;
        Debug.Log(_inventory.transform.position);

        _inventory.transform.position = Input.mousePosition;
    }

    public void MoveToggle(bool state)
    {
        _canMove = (state);
    }
}
