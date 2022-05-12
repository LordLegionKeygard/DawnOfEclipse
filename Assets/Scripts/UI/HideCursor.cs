using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private void OnEnable()
    {
        CustomEvents.FireHideCursor(true);
    }

    private void OnDisable()
    {
        CustomEvents.FireHideCursor(false);
    }
}
