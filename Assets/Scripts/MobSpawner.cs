using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;

    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionLayer;

    public bool spawn = true;

    private void FixedUpdate()
    {
        SpawnRadius();
    }

    private void SpawnRadius()
    {
        if (spawn)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    SpawnObject();
                    spawn = false;
                }
            }
        }

    }

    public void SpawnObject()
    {
        float spawnTime = Random.Range(10, 25);
        StartCoroutine(ExecuteAfterTime(spawnTime));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            var mySpawnObject = Instantiate(spawnObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.localRotation);
            mySpawnObject.transform.parent = gameObject.transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
