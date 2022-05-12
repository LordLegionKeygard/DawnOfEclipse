using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;
    public void PickUp()
    {
        if (_item.IsStackable)
        {
            for (int i = 0; i < Inventory.InventoryStatic.InventorySlots.Length; i++)
            {
                if (Inventory.InventoryStatic.InventorySlots[i].Item.name == _item.name)
                {
                    Inventory.InventoryStatic.UpdateUI(_item.name);
                    Destroy(gameObject);
                    return;
                }
            }
        }

        if (Inventory.InventoryStatic.FullInventory) return;
        
        Debug.Log("Picking up " + _item.name);
        Inventory.InventoryStatic.Add(_item);
        Destroy(gameObject);
        return;
    }
}
