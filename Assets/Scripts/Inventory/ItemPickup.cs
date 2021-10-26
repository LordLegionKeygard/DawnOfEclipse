using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{
    public Item item;
    private Inventory inventory;

    private InventoryUI inventoryUI;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();

        if (item.name == "HealthPotion")
        {          
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == "HealthPotion")
                {
                    inventory.items[i].amount = 5;
                    inventoryUI.UpdateUI();
                    Destroy(gameObject);
                    return;
                }
            }
        }
        else if (item.name == "SpeedPotion")
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == "SpeedPotion")
                {
                    inventory.items[i].amount = 5;
                    inventoryUI.UpdateUI();
                    Destroy(gameObject);
                    return;
                }
            }
        }
        else if (item.name == "ManaPotion")
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == "ManaPotion")
                {
                    inventory.items[i].amount = 5;
                    inventoryUI.UpdateUI();
                    Destroy(gameObject);
                    return;
                }
            }
        }
        Inventory.instance.Add(item);
        Destroy(gameObject);

    }
}
