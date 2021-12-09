using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent;
    [SerializeField] private GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)           
                slots[i].AddItem(inventory.items[i]);           
            else           
                slots[i].ClearSlot();           
        }
    }
}

