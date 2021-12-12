using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public bool isCursor;
    [SerializeField] private Image icon;
    public TextMeshProUGUI amount;
    [SerializeField] private Button removeButton;
    [SerializeField] private GameObject ringFromBtn;
    private PlayerAnimatorManager playerAnimatorManager;
    public Item item;
    private Inventory inventory;
    private InventorySlot[] inventorySlots;
    private PotionsControl potionsControl;
    private bool canDrinkAnyPotions = true;

    private void Start()
    {
        potionsControl = FindObjectOfType<PotionsControl>();
        playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
        inventory = FindObjectOfType<Inventory>();
        inventorySlots = FindObjectsOfType<InventorySlot>();
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
            if (item.isUsedItem && canDrinkAnyPotions)
            {
                item.amount--;
                if(item.amount == 0) amount.enabled = false;
                amount.text = item.amount.ToString();
                CantDrinkAnyPotions();
                switch (item.name)
                {
                    case ("HealthPotion"):
                        {
                            playerAnimatorManager.Drinking(0);
                            potionsControl.UsePotions(1);

                            break;
                        }
                    case ("SpeedPotion"):
                        {
                            playerAnimatorManager.Drinking(1);
                            potionsControl.UsePotions(2);
                            break;
                        }
                    case ("ManaPotion"):
                        {
                            playerAnimatorManager.Drinking(2);
                            potionsControl.UsePotions(3);
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
    private void CantDrinkAnyPotions()
    {
        foreach (var pot in inventorySlots)
        {
            pot.canDrinkAnyPotions = false;
        }
        StartCoroutine(ExecuteAfterTime(3f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            foreach (var pot in inventorySlots)
            {
                pot.canDrinkAnyPotions = true;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var pot in inventorySlots)
        {
            pot.canDrinkAnyPotions = true;
        }
    }
}
