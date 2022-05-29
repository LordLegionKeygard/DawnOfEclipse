using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    // [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0);

    // [SerializeField] private Vector3 _randomizeIntensity = new Vector3(0.5f, 0, 0);

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
        // transform.localPosition +=_offset;
        // transform.localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x), 0, 0);
    }

    private void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Camera.main.transform.position);
    }
}
