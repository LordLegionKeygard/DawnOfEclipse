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

    public static Action OnCameraLockOnTargetDeath;
    public static void FireCameraLockOnTargetDeath()
    {
        OnCameraLockOnTargetDeath?.Invoke();
    }

    public static Action<bool> OnEnabledDamageCollider;
    public static void FireEnabledDamageCollider(bool state)
    {
        OnEnabledDamageCollider?.Invoke(state);
    }

    public static Action<int> OnChangeIKHands;
    public static void FireChangeIKHands(int number)
    {
        OnChangeIKHands?.Invoke(number);
    }

    public static Action<bool> OnCanWalk;
    public static void FireCanWalk(bool state)
    {
        OnCanWalk?.Invoke(state);
    }

    public static Action<bool> OnCanRoot;
    public static void FireCanRoot(bool state)
    {
        OnCanRoot?.Invoke(state);
    }

    public static Action<bool> OnBlock;
    public static void FireBlock(bool state)
    {
        OnBlock?.Invoke(state);
    }

    public static Action OnPlayerDeath;
    public static void FirePlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static Action<bool> OnCameraCanMove;
    public static void FireCameraCanMove(bool state)
    {
        OnCameraCanMove?.Invoke(state);
    }

    public static Action<bool> OnPickUp;
    public static void FirePickUp(bool state)
    {
        OnPickUp?.Invoke(state);
    }

    public static Action OnCharacterCreate;
    public static void FireCharacterCreate()
    {
        OnCharacterCreate?.Invoke();
    }
    public static Action<bool> OnActiveTargetSkill;
    public static void FireActiveTargetSkill(bool state)
    {
        OnActiveTargetSkill?.Invoke(state);
    }

    public static Action OnUseTargetSkill;
    public static void FireUseTargetSkill()
    {
        OnUseTargetSkill?.Invoke();
    }

    public static Action<bool> OnPoisonHandsParticle;
    public static void FirePoisonHandsParticle(bool state)
    {
        OnPoisonHandsParticle?.Invoke(state);
    }

    public static Action<bool> OnPlayerInWaterVFX;
    public static void FirePlayerInWaterVFX(bool state)
    {
        OnPlayerInWaterVFX?.Invoke(state);
    }

    public static Action<int> OnChangeExperience;
    public static void FireChangeExperience(int number)
    {
        OnChangeExperience?.Invoke(number);
    }
    public static Action OnUpdateAllStats;
    public static void FireUpdateAllStats()
    {
        OnUpdateAllStats?.Invoke();
    }

    public static Action<bool> OnCalculateAllStats; //start = true, buff = false
    public static void FireCalculateAllStats(bool state)
    {
        OnCalculateAllStats?.Invoke(state);
    }

    public static Action OnUpdateEnemyNameColorText;
    public static void FireUpdateEnemyNameColorText()
    {
        OnUpdateEnemyNameColorText?.Invoke();
    }

    public static Action<int> OnUpdateWeaponPhysDamage;
    public static void FireUpdateWeaponPhysDamage(int number)
    {
        OnUpdateWeaponPhysDamage?.Invoke(number);
    }

    public static Action<int> OnUpdateWeaponMageDamage;
    public static void FireUpdateWeaponMageDamage(int number)
    {
        OnUpdateWeaponMageDamage?.Invoke(number);
    }

    public static Action<float> OnUpdateWeaponPhysCritChance;
    public static void FireUpdateWeaponPhysCritChance(float number)
    {
        OnUpdateWeaponPhysCritChance?.Invoke(number);
    }

    public static Action<float> OnUpdateWeaponMageCritChance;
    public static void FireUpdateWeaponMageCritChance(float number)
    {
        OnUpdateWeaponMageCritChance?.Invoke(number);
    }

    public static Action OnUpdateBaseWeaponDamage;
    public static void FireUpdateBaseWeaponDamage()
    {
        OnUpdateBaseWeaponDamage?.Invoke();
    }

    public static Action<bool> OnHideCursor;
    public static void FireHideCursor(bool state)
    {
        OnHideCursor?.Invoke(state);
    }
    public static Action OnMenuToggle;
    public static void FireMenuToggle()
    {
        OnMenuToggle?.Invoke();
    }

    public static Action<bool> OnCanUseSkill;
    public static void FireCanUseSkill(bool state)
    {
        OnCanUseSkill?.Invoke(state);
    }

    public static Action<bool> OnUseSkillR1;
    public static void FireUseSkillR1(bool state)
    {
        OnUseSkillR1?.Invoke(state);
    }
    public static Action<bool> OnUseSkillR2;
    public static void FireUseSkillR2(bool state)
    {
        OnUseSkillR2?.Invoke(state);
    }

    public static Action<bool> OnAim;
    public static void FireAim(bool state)
    {
        OnAim?.Invoke(state);
    }
    public static Action<bool> OnAimImageToggle;
    public static void FireAimImageToggle(bool state)
    {
        OnAimImageToggle?.Invoke(state);
    }

    public static Action<bool> OnCanRotate;
    public static void FireCanRotate(bool state)
    {
        OnCanRotate?.Invoke(state);
    }

    public static Action<int> OnUseMana;
    public static void FireUseMana(int number)
    {
        OnUseMana?.Invoke(number);
    }

    public static Action OnDropStaff;
    public static void FireDropStaff()
    {
        OnDropStaff?.Invoke();
    }
    public static Action<bool> OnShootArrow;
    public static void FireShootArrow(bool state)
    {
        OnShootArrow?.Invoke(state);
    }

    public static Action OnTakeArrow;
    public static void FireTakeArrow()
    {
        OnTakeArrow?.Invoke();
    }

    public static Action<int> OnUseArrow;
    public static void FireUseArrow(int number)
    {
        OnUseArrow?.Invoke(number);
    }

    public static Action<int> OnTakeTome;
    public static void FireTakeTome(int number)
    {
        OnTakeTome?.Invoke(number);
    }
    public static Action<int, int> OnStatBuff;
    public static void FireStatBuff(int statsNumber, int amount)
    {
        OnStatBuff?.Invoke(statsNumber, amount);
    }

    public static Action<int, int> OnElementalArmorBuff;
    public static void FireElementalArmorBuff(int statsNumber, int amount)
    {
        OnElementalArmorBuff?.Invoke(statsNumber, amount);
    }

    public static Action<int> OnReturnPlayerStats;
    public static void FireReturnPlayerStats(int statsNumber)
    {
        OnReturnPlayerStats?.Invoke(statsNumber);
    }

    public static Action<int> OnCheckIdenticalBuff;
    public static void FireCheckIdenticalBuff(int number)
    {
        OnCheckIdenticalBuff?.Invoke(number);
    }
}
