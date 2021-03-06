using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    public Equipment[] DefaultEquipment;
    public EquipSlotListener _leftHandSlotListener;
    [SerializeField] private EquipSlot[] _equipSlot;
    [SerializeField] private SkinnedMeshRenderer[] _targetsMesh;
    [SerializeField] private Transform[] _weaponsAttachPoints;
    [SerializeField] private Animator _anim;
    [SerializeField] private Image _shieldEquipSlotImage;
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;
    public SkinnedMeshRenderer[] _currentMeshes;
    public GameObject[] _currentGameObject;
    private Equipment[] _currentEquipment;
    private WeaponsInfo _weaponsInfo;
    private bool _twoHandWeaponNow;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    private OnEquipmentChanged _onEquipmentChanged;
    private Inventory _inventory;

    private void Awake()
    {
        Instance = this;
        _weaponsInfo = GetComponent<WeaponsInfo>();
    }

    private void Start()
    {
        _inventory = Inventory.InventoryStatic;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
        _currentMeshes = new SkinnedMeshRenderer[numSlots];
        _currentGameObject = new GameObject[numSlots];
        EquipDefaults();
    }

    public void Equip(Equipment newItem)
    {
        if (newItem.weapon == true)
        {
            Equipment oldGreatSword = Unequip(19);
            Equipment oldStraightSword = Unequip(20);
            Equipment oldHammer = Unequip(27);
            Equipment oldDaggers = Unequip(28);
            Equipment oldStaff = Unequip(29);
            Equipment oldBow = Unequip(30);
            Equipment oldSpear = Unequip(32);
            Equipment oldClaws = Unequip(33);
            if (newItem.twoHandedWeapon == true)
            {
                _twoHandWeaponNow = true;
                Equipment oldShield = Unequip(21);
                Equipment oldQuiver = Unequip(31);
            }
        }
        if (newItem.extraItem == true)
        {
            Equipment oldGreatSword = Unequip(19);
            Equipment oldSpear = Unequip(32);
            Equipment oldDaggers = Unequip(28);
            Equipment oldQuiverArrow = Unequip(31);
            Equipment oldShield = Unequip(21);
            Equipment oldClaws = Unequip(33);

        }

        int slotIndex = (int)newItem.equipSlot;

        if (newItem.equipSlot == EquipmentSlot.LeftRing)
        {
            if (_magicArmorControl.RightRingMagicArmor == DefaultArmor.Ring)
            {
                Unequip(slotIndex + 1);
                _currentEquipment[slotIndex + 1] = newItem;
            }
            else
            {
                Unequip(slotIndex);
                _currentEquipment[slotIndex] = newItem;
            }
            AttachToMesh(newItem, slotIndex);
            return;
        }

        if (newItem.equipSlot == EquipmentSlot.LeftEarring)
        {
            if (_magicArmorControl.RightEarringMagicArmor == DefaultArmor.Earring)
            {
                Unequip(slotIndex + 1);
                _currentEquipment[slotIndex + 1] = newItem;
            }
            else
            {
                Unequip(slotIndex);
                _currentEquipment[slotIndex] = newItem;
            }
            AttachToMesh(newItem, slotIndex);
            return;
        }

        if (_currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = Unequip(slotIndex);

            if (_onEquipmentChanged != null)
            {
                _onEquipmentChanged.Invoke(newItem, oldItem);
            }
        }

        _currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    public void UnequipTwoHandedWeaponFromShield()
    {
        _leftHandSlotListener.enabled = true;
        _equipSlot[17].BackIcon.enabled = true;
        _equipSlot[17].Icon.enabled = false;
    }
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;

        if (_currentEquipment[slotIndex] != null)
        {
            oldItem = _currentEquipment[slotIndex];
            _inventory.Add(oldItem);

            if (slotIndex == 19 || slotIndex == 20 || slotIndex == 27 || slotIndex == 28 || slotIndex == 29 || slotIndex == 30 || slotIndex == 32 || slotIndex == 33)
            {
                _weaponsInfo.NoWeapon();

                CustomEvents.FireChangeIKHands(0);
                if (slotIndex == 19 || slotIndex == 32)
                {
                    _equipSlot[17].BackIcon.enabled = true;
                    _equipSlot[17].Icon.enabled = false;
                }

                if (slotIndex == 28 || slotIndex == 33) //Dual weapons
                {
                    Destroy(_currentGameObject[19].gameObject);
                    _equipSlot[17].BackIcon.enabled = true;
                    _equipSlot[17].Icon.enabled = false;
                }
                if (slotIndex == 30)
                {
                    _equipSlot[16].BackIcon.enabled = true;
                    _equipSlot[16].Icon.enabled = false;
                    ResetAnimator();
                }
                Destroy(_currentGameObject[slotIndex].gameObject);
                Debug.Log("DESTROY " + _currentGameObject[slotIndex].gameObject);
            }

            if (slotIndex == 21 || slotIndex == 31) // extraItem
            {
                Destroy(_currentGameObject[slotIndex].gameObject);
                _equipSlot[17].BackIcon.enabled = true;
                _equipSlot[17].Icon.enabled = false;
            }

            if (_currentMeshes[slotIndex] != null)
            {
                Destroy(_currentMeshes[slotIndex].gameObject);
                if (slotIndex == 6 || slotIndex == 4 || slotIndex == 8 || slotIndex == 10 || slotIndex == 13 || slotIndex == 15 || slotIndex == 18)
                {
                    Destroy(_currentMeshes[slotIndex - 1].gameObject);
                }
            }

            _currentEquipment[slotIndex] = null;

            if (_onEquipmentChanged != null) { _onEquipmentChanged.Invoke(null, oldItem); }
            return oldItem;
        }
        return null;
    }

    private void BoneTransformArmor(SkinnedMeshRenderer skin, int targetMeshNumber, int slot, bool doubleItem)
    {
        skin.transform.parent = _targetsMesh[targetMeshNumber].transform.parent;
        skin.rootBone = _targetsMesh[targetMeshNumber].rootBone;
        skin.bones = _targetsMesh[targetMeshNumber].bones;
        if (!doubleItem)
            _currentMeshes[slot] = skin;
        else
            _currentMeshes[slot - 1] = skin;
    }

    private void BoneTransformWeapon(GameObject weapon, int slot, Transform weaponPoint)
    {
        weapon.transform.parent = weaponPoint.transform.parent;
        weapon.transform.position = weaponPoint.position;
        weapon.transform.rotation = weaponPoint.rotation;
        _currentGameObject[slot] = weapon;
    }

    private void EquipSlotAndIcon(int equipSlotNumber, Equipment equipItem)
    {
        if (equipItem.icon != null)
        {
            _equipSlot[equipSlotNumber].Icon.sprite = equipItem.icon;
            _equipSlot[equipSlotNumber].EquipIcon();
            _equipSlot[equipSlotNumber].Item = equipItem;
            Debug.Log("EquipSlotNumber = " + equipSlotNumber, _equipSlot[equipSlotNumber].Item);
        }
    }

    private void AttachToMesh(Equipment item, int slotIndex)
    {
        switch (item.equipSlot)
        {
            case EquipmentSlot.Torso:
                SkinnedMeshRenderer newMeshTorso = Instantiate(item.Meshes[CharacterInformation.Gender]);
                BoneTransformArmor(newMeshTorso, 0, slotIndex, false);
                _armorControl.TorsoArmor = item.armorModifier;
                EquipSlotAndIcon(1, item);
                break;
            case EquipmentSlot.Hands:
                SkinnedMeshRenderer newMeshHandLeft = Instantiate(item.Meshes[CharacterInformation.Gender]);
                SkinnedMeshRenderer newMeshHandRight = Instantiate(item.Meshes[CharacterInformation.Gender + 1]);
                BoneTransformArmor(newMeshHandLeft, 2, slotIndex, false);
                BoneTransformArmor(newMeshHandRight, 1, slotIndex, true);
                _armorControl.HandsArmor = item.armorModifier;
                EquipSlotAndIcon(4, item);
                break;
            case EquipmentSlot.ArmUppers:
                SkinnedMeshRenderer newMeshArmUpperLeft = Instantiate(item.Meshes[CharacterInformation.Gender]);
                SkinnedMeshRenderer newMeshArmUpperRight = Instantiate(item.Meshes[CharacterInformation.Gender + 1]);
                BoneTransformArmor(newMeshArmUpperLeft, 4, slotIndex, false);
                BoneTransformArmor(newMeshArmUpperRight, 3, slotIndex, true);
                _armorControl.ArmUppers = item.armorModifier;
                EquipSlotAndIcon(5, item);
                break;
            case EquipmentSlot.ArmLowers:
                SkinnedMeshRenderer newMeshArmLowerLeft = Instantiate(item.Meshes[CharacterInformation.Gender]);
                SkinnedMeshRenderer newMeshArmLowerRight = Instantiate(item.Meshes[CharacterInformation.Gender + 1]);
                if (CharacterInformation.Gender == 0)
                {
                    BoneTransformArmor(newMeshArmLowerLeft, 6, slotIndex, false);
                    BoneTransformArmor(newMeshArmLowerRight, 5, slotIndex, true);
                }
                if (CharacterInformation.Gender == 2)
                {
                    BoneTransformArmor(newMeshArmLowerLeft, 24, slotIndex, false);
                    BoneTransformArmor(newMeshArmLowerRight, 25, slotIndex, true);
                }
                _armorControl.ArmLowers = item.armorModifier;
                EquipSlotAndIcon(6, item);
                break;
            case EquipmentSlot.Hips:
                SkinnedMeshRenderer newMeshHips = Instantiate(item.Meshes[CharacterInformation.Gender]);
                if (CharacterInformation.Gender == 0) { BoneTransformArmor(newMeshHips, 7, slotIndex, false); }
                if (CharacterInformation.Gender == 2) { BoneTransformArmor(newMeshHips, 21, slotIndex, false); }
                _armorControl.HipsArmor = item.armorModifier;
                EquipSlotAndIcon(2, item);
                break;
            case EquipmentSlot.Legs:
                SkinnedMeshRenderer newMeshLegLeft = Instantiate(item.Meshes[CharacterInformation.Gender]);
                SkinnedMeshRenderer newMeshLegRight = Instantiate(item.Meshes[CharacterInformation.Gender + 1]);
                if (CharacterInformation.Gender == 0)
                {
                    BoneTransformArmor(newMeshLegLeft, 9, slotIndex, false);
                    BoneTransformArmor(newMeshLegRight, 8, slotIndex, true);
                }
                if (CharacterInformation.Gender == 2)
                {
                    BoneTransformArmor(newMeshLegLeft, 23, slotIndex, false);
                    BoneTransformArmor(newMeshLegRight, 22, slotIndex, true);
                }
                _armorControl.LegsArmor = item.armorModifier;
                EquipSlotAndIcon(3, item);
                break;
            case EquipmentSlot.BackAttachment:
                SkinnedMeshRenderer newMeshBackAttachment = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshBackAttachment, 10, slotIndex, false);
                _magicArmorControl.BackAttachmentMagicArmor = item.MagicArmorModifier;
                EquipSlotAndIcon(7, item);
                break;
            case EquipmentSlot.Shoulders:
                SkinnedMeshRenderer newMeshShoulderLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshShoulderLeft, 12, slotIndex, false);
                SkinnedMeshRenderer newMeshShoulderRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshShoulderRight, 11, slotIndex, true);
                _armorControl.Shoulders = item.armorModifier;
                EquipSlotAndIcon(8, item);
                break;
            // case EquipmentSlot.HeadSlot:

            //     if (item.hatNumber == 0)
            //     {
            //         SkinnedMeshRenderer newMeshHeadCoveringsBaseHair = Instantiate(item.Meshes[0]);
            //         BoneTransformArmor(newMeshHeadCoveringsBaseHair, 13, slotIndex, false);
            //     }
            //     if (item.hatNumber == 1)
            //     {
            //         SkinnedMeshRenderer newMeshHeadCoveringNoHair = Instantiate(item.Meshes[0]);
            //         BoneTransformArmor(newMeshHeadCoveringNoHair, 14, slotIndex, false);
            //     }
            //     if (item.hatNumber == 2)
            //     {
            //         SkinnedMeshRenderer newMeshHeadNoElememts = Instantiate(item.Meshes[0]);
            //         BoneTransformArmor(newMeshHeadNoElememts, 15, slotIndex, false);
            //     }
            //     EquipSlotAndIcon(0, item);
            //     _armorControl.HeadSlotArmor = item.armorModifier;
            //     break;
            case EquipmentSlot.Elbows:
                SkinnedMeshRenderer newMeshElbowLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshElbowLeft, 17, slotIndex, false);
                SkinnedMeshRenderer newMeshElbowRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshElbowRight, 16, slotIndex, true);
                _armorControl.Elbows = item.armorModifier;
                EquipSlotAndIcon(9, item);
                break;
            case EquipmentSlot.Knees:
                SkinnedMeshRenderer newMeshKneeLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshKneeLeft, 20, slotIndex, false);
                SkinnedMeshRenderer newMeshKneeRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshKneeRight, 19, slotIndex, true);
                _armorControl.Knees = item.armorModifier;
                EquipSlotAndIcon(10, item);
                break;
            case EquipmentSlot.GreatSword:
                CustomEvents.FireChangeIKHands(1);
                _weaponsInfo.GreatSword();
                _anim.runtimeAnimatorController = Resources.Load("Animation/GreatSwordController") as RuntimeAnimatorController;
                GameObject newWeaponGreatSwordPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponGreatSwordPrefab, slotIndex, _weaponsAttachPoints[0]);
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _armorControl.ShieldArmorPassive = 0;
                _leftHandSlotListener.enabled = false;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(16, item);
                EquipSlotAndIcon(17, item);
                _twoHandWeaponNow = true;
                break;
            case EquipmentSlot.Spear:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Spear();
                _anim.runtimeAnimatorController = Resources.Load("Animation/SpearController") as RuntimeAnimatorController;
                GameObject newWeaponSpearPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponSpearPrefab, slotIndex, _weaponsAttachPoints[7]);
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _armorControl.ShieldArmorPassive = 0;
                _leftHandSlotListener.enabled = false;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(16, item);
                EquipSlotAndIcon(17, item);
                _twoHandWeaponNow = true;
                break;
            case EquipmentSlot.DualDaggers:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Daggers();
                _anim.runtimeAnimatorController = Resources.Load("Animation/DualDaggersController") as RuntimeAnimatorController;
                GameObject newDualDaggersRightPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newDualDaggersRightPrefab, 19, _weaponsAttachPoints[1]);
                GameObject newDualDaggersLeftPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newDualDaggersLeftPrefab, slotIndex, _weaponsAttachPoints[3]);
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _armorControl.ShieldArmorPassive = 0;
                _leftHandSlotListener.enabled = false;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(16, item);
                EquipSlotAndIcon(17, item);
                _twoHandWeaponNow = true;
                break;
            case EquipmentSlot.Claws:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Claws();
                _anim.runtimeAnimatorController = Resources.Load("Animation/ClawsController") as RuntimeAnimatorController;
                GameObject newClawsRightPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newClawsRightPrefab, 19, _weaponsAttachPoints[8]);
                GameObject newClawsLeftPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newClawsLeftPrefab, slotIndex, _weaponsAttachPoints[9]);
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _armorControl.ShieldArmorPassive = 0;
                _leftHandSlotListener.enabled = false;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(16, item);
                EquipSlotAndIcon(17, item);
                _twoHandWeaponNow = true;
                break;
            case EquipmentSlot.StraightSword:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.StraightSword();
                _anim.runtimeAnimatorController = Resources.Load("Animation/SwordController") as RuntimeAnimatorController;
                GameObject newWeaponStraightSwordPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponStraightSwordPrefab, slotIndex, _weaponsAttachPoints[1]);
                _equipSlot[16].Icon.sprite = item.icon;
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                EquipSlotAndIcon(16, item);
                _twoHandWeaponNow = false;
                break;
            case EquipmentSlot.Bow:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Bow();
                Unequip(21);
                _anim.runtimeAnimatorController = Resources.Load("Animation/BowController") as RuntimeAnimatorController;
                GameObject newBowPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newBowPrefab, slotIndex, _weaponsAttachPoints[5]);
                _equipSlot[16].Icon.sprite = item.icon;
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                EquipSlotAndIcon(16, item);
                _twoHandWeaponNow = false;
                break;
            case EquipmentSlot.Hammer:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Hammer();
                _anim.runtimeAnimatorController = Resources.Load("Animation/HammerController") as RuntimeAnimatorController;
                GameObject newWeaponHammerPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponHammerPrefab, slotIndex, _weaponsAttachPoints[1]);
                _equipSlot[16].Icon.sprite = item.icon;
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                EquipSlotAndIcon(16, item);
                _twoHandWeaponNow = false;
                break;
            case EquipmentSlot.Staff:
                CustomEvents.FireChangeIKHands(0);
                _weaponsInfo.Staff();
                _anim.runtimeAnimatorController = Resources.Load("Animation/StaffController") as RuntimeAnimatorController;
                GameObject newWeaponStaffPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponStaffPrefab, slotIndex, _weaponsAttachPoints[4]);
                _equipSlot[16].Icon.sprite = item.icon;
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                EquipSlotAndIcon(16, item);
                _twoHandWeaponNow = false;
                break;
            case EquipmentSlot.Shield:
                CustomEvents.FireChangeIKHands(0);
                if (_twoHandWeaponNow == true)
                {
                    ResetAnimator();
                    _equipSlot[16].BackIcon.enabled = true;
                    _equipSlot[16].Icon.enabled = false;
                }
                Unequip(19);
                Unequip(30);
                Unequip(31);
                _leftHandSlotListener.enabled = true;
                GameObject newShieldPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newShieldPrefab, slotIndex, _weaponsAttachPoints[2]);
                _armorControl.ShieldArmorPassive = item.armorModifier;
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 1f);
                _twoHandWeaponNow = false;
                EquipSlotAndIcon(17, item);
                break;

            case EquipmentSlot.QuiverArrow:
                if (_twoHandWeaponNow == true)
                {
                    ResetAnimator();
                    _equipSlot[16].BackIcon.enabled = true;
                    _equipSlot[16].Icon.enabled = false;
                }
                Unequip(19);
                Unequip(21);
                _leftHandSlotListener.enabled = true;
                GameObject newQuiverArrowPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newQuiverArrowPrefab, slotIndex, _weaponsAttachPoints[6]);
                _equipSlot[17].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 1f);
                _twoHandWeaponNow = false;
                EquipSlotAndIcon(17, item);
                break;
            case EquipmentSlot.LeftRing:
                if (_magicArmorControl.RightRingMagicArmor == 0)
                {
                    EquipSlotAndIcon(12, item);
                    _magicArmorControl.RightRingMagicArmor = item.MagicArmorModifier;
                }
                else
                {
                    EquipSlotAndIcon(11, item);
                    _magicArmorControl.LeftRingMagicArmor = item.MagicArmorModifier;
                }
                break;
            case EquipmentSlot.LeftEarring:
                if (_magicArmorControl.RightEarringMagicArmor == 0)
                {
                    EquipSlotAndIcon(14, item);
                    _magicArmorControl.RightEarringMagicArmor = item.MagicArmorModifier;
                }
                else
                {
                    EquipSlotAndIcon(13, item);
                    _magicArmorControl.LeftEarringMagicArmor = item.MagicArmorModifier;
                }
                break;
            case EquipmentSlot.Necklace:
                EquipSlotAndIcon(15, item);
                _magicArmorControl.NecklaceMagicArmor = item.MagicArmorModifier;
                break;
        }
        _armorControl.UpdateArmor();
        _magicArmorControl.UpdateMagicArmor();
    }

    public void ResetAnimator()
    {
        _anim.runtimeAnimatorController = Resources.Load("Animation/MainController") as RuntimeAnimatorController;
    }

    private void EquipDefaults()
    {
        foreach (Equipment e in DefaultEquipment)
        {
            Equip(e);
            _weaponsInfo.NoWeapon();
            ResetAnimator();
        }
    }
}