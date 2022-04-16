using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputController _playerInputController;
    private StaminaControl _staminaControl;

    private float _nextJumpTime = 1.5f;
    private void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _staminaControl = GetComponent<StaminaControl>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!_playerInputController.IsCanJump)
        {
            _nextJumpTime -= Time.deltaTime;
            if(_nextJumpTime < 0)
            {
                _playerInputController.IsCanJump = true;
                _nextJumpTime = 1.5f;
            }
        }
    }

    public void CanJump()
    {
        _playerInputController.IsCanJump = true;
    }

    public void Roll()
    {
        _playerInputController.IsBlock = true;
        StartCoroutine(ExecuteAfterTime(1f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _playerInputController.IsBlock = false;
        }
    }
}