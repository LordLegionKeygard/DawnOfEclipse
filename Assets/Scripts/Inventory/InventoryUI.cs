using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}

