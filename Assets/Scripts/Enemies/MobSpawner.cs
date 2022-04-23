using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private float _detectionRadius = 180f;
    [SerializeField] private LayerMask _detectionLayer;
    public bool CanSpawn = true;

    private void FixedUpdate()
    {
        if (!CanSpawn) return;
        SpawnRadius();
    }

    private void SpawnRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                SpawnObject();
                CanSpawn = false;
            }
        }
    }

    public void SpawnObject()
    {
        float spawnTime = Random.Range(_spawnTime, _spawnTime + 10);
        StartCoroutine(ExecuteAfterTime(spawnTime));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            var mySpawnObject = Instantiate(_spawnObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.localRotation);
            mySpawnObject.transform.parent = gameObject.transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }
}
