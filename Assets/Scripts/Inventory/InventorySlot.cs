using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Add item to the slot

    // private void Awake()
    // {
    //     item = new Item();
    // }

    private void Start()
    {
        playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
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
        icon.enabled = true;
        removeButton.interactable = true;
        ringFromBtn.SetActive(true);
        if (item.maxStack > 1)
        {
            amount.enabled = true;
            amount.text = item.amount.ToString();
        }
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
            }
            item.Use();
        }
    }
}
