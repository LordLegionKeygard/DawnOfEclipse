using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerActions;
    [SerializeField] private GameObject _inventory;

    private void Update()
    {
        if((Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E))) //What??
            FalseAllActions();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") && !_inventory.activeInHierarchy)
            _playerActions[0].SetActive(true);      
    }

    private void OnTriggerExit(Collider other){FalseAllActions();}

    private void FalseAllActions()
    {
        foreach (var action in _playerActions)
            action.SetActive(false);
    }
}
