using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{
    public Item item;

    Inventory inventory;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);

        if (item.name == "HealthPotion")
        {
            inventory = FindObjectOfType<Inventory>();
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == "HealthPotion")
                {
                    inventory.items[i].amount = 5;
                    Destroy(gameObject);
                    return;
                }
            }
        }
        Inventory.instance.Add(item);
        Destroy(gameObject);

    }
}
