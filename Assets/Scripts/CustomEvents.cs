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
}
