using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public int QuiverArrow;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        CustomEvents.FireUseArrow(QuiverArrow);
    }

    private void OnDestroy()
    {
        CustomEvents.FireUseArrow(0);
    }
}
