using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [HideInInspector] public Item CurrentItem; // Current item in the slot
    public Sprite icon = null;
    public int maxStack;
    public int amount;
    public bool isDefaultItem = false;
    public bool isStackable { get { return (maxStack > 1); } }
    public bool hasItem { get { return (CurrentItem != null); } }
    private InventorySlot inventorySlot;

    public bool isUsedItem = false;
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItemFromInventoryList(this);
    }


    // public int amount
    // {
    //     get { return _amount; }
    //     set
    //     {
    //         if (item == null) _amount = 0;
    //         else if (amount > item.maxStack) _amount = item.maxStack;
    //         else if (value < 1) _amount = 0;
    //         else _amount = value;
    //         RefreshUISlot();
    //     }
    // }


    public static bool Compare(Item slotA, Item slotB)
    {
        if (slotA.CurrentItem != slotB.CurrentItem)
            return false;

        return true;
    }

    public static void Swap(Item slotA, Item slotB)
    {
        Debug.Log("Swap");
        Item _item = slotA.CurrentItem;
        int _amount = slotA.CurrentItem.amount;

        slotA.CurrentItem = slotB.CurrentItem;
        slotA.amount = slotB.amount;

        slotB.CurrentItem = _item;
        slotB.amount = _amount;

        slotA.RefreshUISlot();
        slotB.RefreshUISlot();
    }

    public void Clear()
    {
        CurrentItem = null;
        amount = 0;
        RefreshUISlot();
    }

    public void AttachUI(InventorySlot uiSlot)
    {
        inventorySlot = uiSlot;
        inventorySlot.item = this;
        RefreshUISlot();
    }
    public void DetatchUI()
    {
        inventorySlot.ClearSlot();
        inventorySlot = null;
    }

    public bool isAttachedToUI { get { return (inventorySlot != null); } }

    public void RefreshUISlot()
    {
        if (!isAttachedToUI)
            return;

        inventorySlot.RefreshSlot();
    }
}