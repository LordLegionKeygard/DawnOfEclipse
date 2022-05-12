using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    public Image Icon;
    public Image ItemFrame;
    public SelectItemInfo SelectItemInfo;
    public bool IsItemSelect = false;
    public Item Item;

    public void UpdateSelectItemInfoTransform()
    {
        if (!IsItemSelect) return;
        SelectItemInfo.UpdateTransform(new Vector2(transform.position.x, transform.position.y));
    }
}
