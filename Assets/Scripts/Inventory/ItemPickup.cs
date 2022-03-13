using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;

    public void PickUp()
    {
        Debug.Log("Picking up " + _item.name);
        if (_item.maxStack > 1)
        {
            for (int i = 0; i < Inventory.instance.items.Count; i++)
            {
                if (Inventory.instance.items[i].name == _item.name)
                {
                    Inventory.instance.UpdateUI(_item.name);
                    Destroy(gameObject);
                    return;
                }
            }
        }
        Inventory.instance.Add(_item);
        Destroy(gameObject);
    }
}
