using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;

    public void PickUp()
    {
        for (int k = 0; k < Inventory.InventoryStatic.items.Count; k++)
        {
            if (Inventory.InventoryStatic.items[k].name == "Empty_Item")
            {
                Inventory.InventoryStatic.FullInventory = false;
                if (_item.maxStack > 1)
                {
                    for (int i = 0; i < Inventory.InventoryStatic.items.Count; i++)
                    {
                        if (Inventory.InventoryStatic.items[i].name == _item.name)
                        {
                            Inventory.InventoryStatic.UpdateUI(_item.name);
                            Destroy(gameObject);
                            return;
                        }
                    }
                }
                Debug.Log("Picking up " + _item.name);
                Inventory.InventoryStatic.Add(_item);
                Destroy(gameObject);
                return;
            }
            else
            {
                Inventory.InventoryStatic.FullInventory = true;
            }
        }

    }
}
