// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class ItemContainer : MonoBehaviour
// {
//     public GameObject parentWindow;
//     public Transform contentWindow; //The GridLayoutGroup
//     public TextMeshProUGUI title;

//     public string containerName;

//     GameObject SlotPrefab; // The prefab of the UIItemSlot object;
//     List<Item> items = new List<Item>();

//     private void Start()
//     {
//         // SlotPrefab = Resources.Load<GameObject>("InventorySlot");



//         // Item[] tempItems = new Item[3];
//         // tempItems[0] = Resources.Load<Item>("SM_Wep_Slayer_01");
//         // tempItems[1] = Resources.Load<Item>("HealthPotion");
//         // tempItems[2] = Resources.Load<Item>("HealthPotion");

//         // for (int i = 0; i < 14; i++)
//         // {
//         //     int index = Random.Range(0, 3);
//         //     int amount = Random.Range(1, tempItems[index].maxStack);

//         //     // items.Add(new Item(tempItems[index].name, amount));
//         // }
//     }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Q))
//             CloseContainer();
//         else if (Input.GetKeyDown(KeyCode.W))
//             OpenContainer(items);
//     }

//     List<InventorySlot> UISlots = new List<InventorySlot>();

//     public void OpenContainer(List<Item> slots)
//     {
//         parentWindow.SetActive(true);
//         title.text = containerName.ToUpper();

//         for (int i = 0; i < slots.Count; i++)
//         {
//             GameObject newSlot = Instantiate(SlotPrefab, contentWindow);

//             newSlot.name = i.ToString();

//             UISlots.Add(newSlot.GetComponent<InventorySlot>());

//             slots[i].AttachUI(UISlots[i]);
//         }
//     }

//     public void CloseContainer()
//     {
//         foreach (InventorySlot slot in UISlots)
//         {
//             slot.item.DetatchUI();
//             Destroy(slot.gameObject);
//         }

//         UISlots.Clear();
//         parentWindow.SetActive(false);
//     }
// }

