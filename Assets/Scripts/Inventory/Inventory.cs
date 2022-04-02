using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory InventoryStatic;

    private void OnEnable()
    {
        CustomEvents.OnCheckFullInventory += CheckFullInventory;
        CheckFullInventory();
    }

    private void Awake()
    {
        if (InventoryStatic != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        InventoryStatic = this;
    }

    #endregion

    public int _space;
    [SerializeField] private Item _emptySlot;
    [SerializeField] private InventorySlot[] _slots;
    public List<Item> items = new List<Item>();

    public bool FullInventory;

    public void Add(Item item)
    {
        if (item.IsDefaultItem) return;

        StartCoroutine(ExecuteAfterTime(0.1f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "Empty_Item")
                {
                    items[i] = item;
                    UpdateUI(item.Name[Language.LanguageNumber]);
                    CheckFullInventory();
                    break;
                }
            }
        }
    }

    public void UpdateUI(string name)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < InventoryStatic.items.Count)
            {
                _slots[i].AddItem(InventoryStatic.items[i], name);
            }

            else
                _slots[i].ClearSlot();
        }
    }

    private void CheckFullInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == "Empty_Item")
            {
                FullInventory = false;
                return;
            }
            else
            {
                FullInventory = true;
            }
        }
    }

    public void RemoveItemFromInventoryList(Item item, int number)
    {
        items[number] = _emptySlot;
        UpdateUI(item.Name[Language.LanguageNumber]);
        CheckFullInventory();
    }

    private void OnDisable()
    {
        CustomEvents.OnCheckFullInventory -= CheckFullInventory;
    }
}