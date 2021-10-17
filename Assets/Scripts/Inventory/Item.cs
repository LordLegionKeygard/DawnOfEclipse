using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int maxStack;
    public bool isStackable { get { return (maxStack > 1); } }
    public bool isUsageItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public Item item; // Current item in the slot

    private InventorySlot inventorySlot;
    public int amount;
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
    public bool hasItem { get { return (item != null); } }

    public static bool Compare(Item slotA, Item slotB)
    {
        if (slotA.item != slotB.item)
            return false;

        return true;
    }

    public static void Swap(Item slotA, Item slotB)
    {
        Debug.Log("Swap");
        Item _item = slotA.item;
        int _amount = slotA.item.amount;

        slotA.item = slotB.item;
        slotA.amount = slotB.amount;

        slotB.item = _item;
        slotB.amount = _amount;

        slotA.RefreshUISlot();
        slotB.RefreshUISlot();
    }

    public void Clear()
    {
        item = null;
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