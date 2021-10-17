using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryClickHandler : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointer;
    [SerializeField] private EventSystem eventSystem;
    public InventorySlot cursor;

    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointer = new PointerEventData(eventSystem);
            pointer.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(pointer, results);

            if (results.Count > 0 && results[0].gameObject.tag == "UIItemSlot")
                ProcessClick(results[0].gameObject.GetComponent<InventorySlot>());
        }
    }

    private void ProcessClick(InventorySlot clicked)
    {
        if (clicked == null)
        {
            return;
        }

        Debug.Log("Worked");

        if (!Item.Compare(cursor.item, clicked.item))
        {
            Item.Swap(cursor.item, clicked.item);
            cursor.RefreshSlot();
            return;
        }
        else
        {
            if (!cursor.item.hasItem)
                return;
            if (!cursor.item.CurrentItem.isStackable)
                return;
            if (clicked.item.amount == clicked.item.CurrentItem.maxStack)
                return;

            int total = cursor.item.amount + clicked.item.amount;
            int maxStack = cursor.item.CurrentItem.maxStack;

            if (total <= maxStack)
            {
                clicked.item.amount = total;
                cursor.item.Clear();
            }
            else
            {
                clicked.item.amount = maxStack;
                cursor.item.amount = total - maxStack;
            }

            cursor.RefreshSlot();
        }

    }
}

