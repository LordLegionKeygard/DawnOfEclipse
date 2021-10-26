using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventorySlot : MonoBehaviour
{
    public bool isCursor;
    [SerializeField] private Image icon;
    public TextMeshProUGUI amount;
    [SerializeField] private Button removeButton;
    [SerializeField] private GameObject ringFromBtn;
    private PlayerAnimatorManager playerAnimatorManager;
    private Item item;
    private Inventory inventory;
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
    public void UseItem()
    {
        if (item != null)
        {
            if (item.isUsedItem)
            {
                item.amount--;
                amount.text = item.amount.ToString();
                switch (item.name)
                {
                    case ("HealthPotion"):
                        {
                            playerAnimatorManager.Drinking(0);
                            Debug.Log("HealthPotion");
                            break;
                        }
                    case ("SpeedPotion"):
                        {
                            playerAnimatorManager.Drinking(1);
                            Debug.Log("SpeedPotion");
                            break;
                        }
                    case ("ManaPotion"):
                        {
                            playerAnimatorManager.Drinking(2);
                            Debug.Log("ManaPotion");
                            break;
                        }
                }
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
}
