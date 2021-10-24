using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    public bool isCursor;
    [SerializeField] private Image icon;            // Reference to the Icon image
    public TextMeshProUGUI amount;
    [SerializeField] private Button removeButton; // Reference to the remove button
    [SerializeField] private GameObject ringFromBtn;
    private PlayerAnimatorManager playerAnimatorManager;
    Item item;  // Current item in the slot
    Inventory inventory;
    private void Start()
    {
        playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (!isCursor) return;

        transform.position = Input.mousePosition;
    }

    public void AddItem(Item newItem)
    {        
        item = newItem;
        icon.sprite = item.icon;
        removeButton.interactable = true;
        icon.enabled = true;
        ringFromBtn.SetActive(true);      
        if (item.maxStack > 1)
        {
            amount.enabled = true;
            amount.text = item.amount.ToString();
        }
        
        // for (int i = 0; i < inventory.items.Count; i++)
        // {
        //     if (inventory.items[i].name == "HealthPotion")
        //     {
        //         int HealthPotionCount = inventory.items.Count(item => item.name == "HealthPotion");

        //         if (HealthPotionCount == 2)
        //         {

        //             //ResetPotionCount();
        //             return;
        //         }
        //     }
        // }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        ringFromBtn.SetActive(false);
        amount.enabled = false;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItemFromInventoryList(item);
        ClearSlot();
    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null)
        {
            if (item.isUsedItem)
            {
                playerAnimatorManager.Drinking();
                item.amount--;
                amount.text = item.amount.ToString();
            }
            if (item.amount == 0)
            {
                if (item.isUsedItem)
                {
                    item.amount = 5;
                }
                item.Use();
            }
        }
    }

    // public void ResetPotionCount()
    // {
    //     item.amount = 5;
    //     amount.text = item.amount.ToString();
    // }
}
