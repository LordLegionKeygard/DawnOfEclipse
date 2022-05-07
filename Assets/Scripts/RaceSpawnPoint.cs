using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _isTeleport;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3[] _raceSpawnPoints;

    private void Start()
    {
        if (!_isTeleport) return;
        TeleportCharacter();
    }

    private void TeleportCharacter()
    {
        switch (CharacterInformation.Race)
        {
            case 0:
                _playerTransform.position = new Vector3(_raceSpawnPoints[0].x, _raceSpawnPoints[0].y, _raceSpawnPoints[0].z);
                break;
            case 1:
                _playerTransform.position = new Vector3(_raceSpawnPoints[1].x, _raceSpawnPoints[1].y, _raceSpawnPoints[1].z);
                break;
        }
    }
}
