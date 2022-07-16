using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotPursue : MonoBehaviour
{
    [SerializeField] private GameObject _character;

    private bool _canPursue = true;

    private void OnEnable()
    {
        CustomEvents.OnCameraCanMove += CanPursue;
    }

    private void LateUpdate()
    {
        if (!_canPursue) return;
        var t = _character.transform;
        transform.position = new Vector3(t.position.x, t.position.y + 1.5f, t.position.z);
    }

    private void CanPursue(bool state)
    {
        if (state) _canPursue = state;

        else
        {
            StartCoroutine(ExecuteAfterTime(0.5f));
            IEnumerator ExecuteAfterTime(float timeInSec)
            {
                yield return new WaitForSeconds(timeInSec);
                _canPursue = state;
            }
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnCameraCanMove -= CanPursue;
    }
}
