using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private Inventory inventory;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
    public void PickUp()
    {
        Debug.Log("Picking up " + item.name);

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
