using System;

public class CustomEvents
{
    public static event Action<int> OnChangeCoins;
    public static void FireChangeCoins(int amount)
    {
        OnChangeCoins?.Invoke(amount);
    }

    public static event Action<int> OnCheckCoins;
    public static void FireCheckCoins(int amount)
    {
        OnCheckCoins?.Invoke(amount);
    }

    public static event Action<int> OnUsePotion;
    public static void FireUsePotion(int amount)
    {
        OnUsePotion?.Invoke(amount);
    }

    public static event Action OnCheckFullInventory;
    public static void FireCheckFullInventory()
    {
        OnCheckFullInventory?.Invoke();
    }

    public static Action<bool> OnSelectItem;
    public static void FireSelectItem(bool state)
    {
        OnSelectItem?.Invoke(state);
    }

    public static Action<bool> OnTooltipToggle;
    public static void FireTooltipToggle(bool state)
    {
        OnTooltipToggle?.Invoke(state);
    }

    public static Action OnUpdateSelectItemTransform;
    public static void FireUpdateSelectItemTransform()
    {
        OnUpdateSelectItemTransform?.Invoke();
    }

    public static Action<int> OnCheckEquipItemSetNumber;
    public static void FireCheckEquipItemSetNumber(int number)
    {
        OnCheckEquipItemSetNumber?.Invoke(number);
    }

    public static Action OnCameraLockOnTarget;
    public static void FireCameraLockOnTarget()
    {
        OnCameraLockOnTarget?.Invoke();
    }
}
