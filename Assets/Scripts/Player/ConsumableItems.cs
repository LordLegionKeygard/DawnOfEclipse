using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItems : MonoBehaviour
{
    public static ConsumableItems ConsumableItemsInstance;
    public int Arrow;

    private void Awake()
    {
        ConsumableItemsInstance = this;
    }

    
}
