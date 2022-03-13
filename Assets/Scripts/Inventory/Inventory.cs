using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public int space = 20;
    [SerializeField] private Item _emptySlot;
    [SerializeField] private InventorySlot[] _slots;
    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (item.isDefaultItem) return;

        if (items.Count >= space)
        {
            Debug.Log("Not enough space in inventory.");
            return;
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == "Empty_Item")
            {
                items[i] = item;
                UpdateUI(item.Name[Language.Number]);
                return;
            }
        }
        return;
    }

    public void UpdateUI(string name)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < instance.items.Count)
            {
                _slots[i].AddItem(instance.items[i], name);
            }

            else
                _slots[i].ClearSlot();
        }
    }

    public void RemoveItemFromInventoryList(Item item, int number)
    {
        items[number] = _emptySlot;
        UpdateUI(item.Name[Language.Number]);
    }
}