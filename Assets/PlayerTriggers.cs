using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerActions;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("one");
        if (other.gameObject.CompareTag("NPC"))        
            _playerActions[0].SetActive(true);        
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var action in _playerActions)       
            action.SetActive(false);        
    }
}
