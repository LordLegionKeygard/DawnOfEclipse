using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    private InventorySlot _inventorySlot;
    public Sprite icon = null;
    public int Price = 250;
    public bool IsDefaultItem;
    public bool IsStackable;
    public bool IsUsedItem;
    public bool IsTome;
    public bool IsSetEffect;
    public string[] Name; //0 eng, 1 rus
    public string[] ItemType;

    [TextArea(1, 3)]
    public string[] ItemInfo;
    public ArmorSetEnum ArmorSetEnum;
    public AllArmorSetInfo AllArmorSetInfo;
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}