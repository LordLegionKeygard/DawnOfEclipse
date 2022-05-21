using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    [SerializeField] private int _quiverArrow;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        CustomEvents.FireUseArrow(_quiverArrow);
    }

    private void OnDestroy()
    {
        CustomEvents.FireUseArrow(0);
    }
}
