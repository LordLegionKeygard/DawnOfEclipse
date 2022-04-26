using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance; //синглтон, не явная зависимость
    public Equipment[] DefaultEquipment;
    public Button ShieldButton;
    [SerializeField] private EquipSlot[] _equipSlot;
    [SerializeField] private GameObject[] _hairEarsHead;
    [SerializeField] private GameObject[] _headAttachment;
    [SerializeField] private SkinnedMeshRenderer[] _targetsMesh;
    [SerializeField] private Transform[] _weaponsAttachPoints;
    [SerializeField] private Animator _anim;
    [SerializeField] private Image _shieldEquipSlotImage;
    [SerializeField] private ArmorControl _armorControl;
    [SerializeField] private MagicArmorControl _magicArmorControl;
    [SerializeField] private MeshFilter _targetMeshFilterWeapon;
    [SerializeField] private MeshFilter _targetMeshFilterShield;
    public SkinnedMeshRenderer[] _currentMeshes;
    public GameObject[] _currentGameObject;
    private Equipment[] _currentEquipment;
    private WeaponTimeCooldown _weaponTimeCooldown;
    private bool _twoHandWeaponNow;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    private OnEquipmentChanged _onEquipmentChanged;
    private Inventory _inventory;

    private void Awake()
    {
        Instance = this;
        _weaponTimeCooldown = GetComponent<WeaponTimeCooldown>();
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
    }
    public void Equip(Equipment newItem)
    {
        if (newItem.canChangehead)
        {
            InActiveAllHeadAttachment();
            switch (newItem.noHair)
            {
                case 0: //withAll
                    AllHeadElementsToggle(true);
                    break;

                case 1:  //noHair & noEars
                    _hairEarsHead[0].SetActive(false);
                    _hairEarsHead[1].SetActive(false);
                    if (CharacterInformation.Gender == 0) { _hairEarsHead[2].SetActive(true); }
                    if (CharacterInformation.Gender == 2) { _hairEarsHead[3].SetActive(true); }
                    break;
                case 2: //fullHelmet
                    AllHeadElementsToggle(false);
                    break;
            }
        }
        if (newItem.FirstAttachmentNumber > 0)
        {
            _headAttachment[newItem.FirstAttachmentNumber].SetActive(true);
        }
        if (newItem.SecondAttachmentNumber > 0)
        {
            _headAttachment[newItem.SecondAttachmentNumber].SetActive(true);
        }
        if (newItem.weapon == true)
        {
            Equipment oldWeapon = Unequip(19);
            Equipment oldWeapon1 = Unequip(20);
            if (newItem.twoHandedWeapon == true)
            {
                _twoHandWeaponNow = true;
                Equipment oldShield = Unequip(21);
            }
        }
        if (newItem.shield == true)
        {
            Equipment oldWeapon = Unequip(19);
        }

        int slotIndex = (int)newItem.equipSlot;

        if (newItem.equipSlot == EquipmentSlot.LeftRing)
        {
            if (_magicArmorControl.RightRingMagicArmor == 0)
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
            if (_magicArmorControl.RightEarringMagicArmor == 0)
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

        Equipment oldItem = Unequip(slotIndex);

        if (_onEquipmentChanged != null)
        {
            _onEquipmentChanged.Invoke(newItem, oldItem);
        }

        _currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    public void AllHeadElementsToggle(bool state)
    {
        _hairEarsHead[0].SetActive(state);
        _hairEarsHead[1].SetActive(state);
        if (CharacterInformation.Gender == 0) { _hairEarsHead[2].SetActive(state); }
        if (CharacterInformation.Gender == 2) { _hairEarsHead[3].SetActive(state); }
    }

    public void InActiveAllHeadAttachment() { foreach (var item in _headAttachment) item.SetActive(false); }

    public void UnequipTwoHandedWeaponFromShield()
    {
        ShieldButton.enabled = true;
        _equipSlot[21].BackIcon.enabled = true;
        _equipSlot[21].Icon.enabled = false;
    }
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;

        if (_currentEquipment[slotIndex] != null)
        {
            oldItem = _currentEquipment[slotIndex];
            _inventory.Add(oldItem);

            if (slotIndex == 19 || slotIndex == 20)
            {
                _weaponTimeCooldown.NoWeapon();
                if (slotIndex == 19)
                {
                    CustomEvents.FireChangeIKHands(0);
                    _equipSlot[21].BackIcon.enabled = true;
                    _equipSlot[21].Icon.enabled = false;
                }
            }

            if (_currentMeshes[slotIndex] != null)
            {
                Destroy(_currentMeshes[slotIndex].gameObject);
                if (slotIndex == 6 || slotIndex == 4 || slotIndex == 8 || slotIndex == 10 || slotIndex == 13 || slotIndex == 15 || slotIndex == 18)
                {
                    Destroy(_currentMeshes[slotIndex - 1].gameObject);
                }
            }
            if (_currentGameObject[slotIndex] != null) { Destroy(_currentGameObject[slotIndex].gameObject); }

            _currentEquipment[slotIndex] = null;

            if (_onEquipmentChanged != null) { _onEquipmentChanged.Invoke(null, oldItem); }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            AllHeadElementsToggle(true);
            Unequip(i);
        }
        CustomEvents.FireChangeIKHands(0);
        _armorControl.ResetArmor();
        _magicArmorControl.ResetArmor();
        EquipDefaults();
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

    private void BoneTransformWeapon(GameObject weapon, int slot, Transform weaponPoint, bool rightHand)
    {
        if (rightHand) { weapon.transform.parent = _targetMeshFilterWeapon.transform.parent; }
        else { weapon.transform.parent = _targetMeshFilterShield.transform.parent; }
        weapon.transform.position = weaponPoint.transform.position;
        weapon.transform.rotation = weaponPoint.transform.rotation;
        _currentGameObject[slot] = weapon;
    }

    private void EquipSlotAndIcon(int equipSlotNumber, Equipment equipItem)
    {
        if (equipItem.icon != null)
        {
            _equipSlot[equipSlotNumber].Icon.sprite = equipItem.icon;
            _equipSlot[equipSlotNumber].EquipIcon();
            _equipSlot[equipSlotNumber].Item = equipItem;
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
                EquipSlotAndIcon(6, item);
                break;
            case EquipmentSlot.ArmUppers:
                SkinnedMeshRenderer newMeshArmUpperLeft = Instantiate(item.Meshes[CharacterInformation.Gender]);
                SkinnedMeshRenderer newMeshArmUpperRight = Instantiate(item.Meshes[CharacterInformation.Gender + 1]);
                BoneTransformArmor(newMeshArmUpperLeft, 4, slotIndex, false);
                BoneTransformArmor(newMeshArmUpperRight, 3, slotIndex, true);
                _armorControl.ArmUpperLeftArmor = item.armorModifier;
                EquipSlotAndIcon(8, item);
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
                _armorControl.ArmLowerLeftArmor = item.armorModifier;
                EquipSlotAndIcon(10, item);
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
                EquipSlotAndIcon(4, item);
                break;
            case EquipmentSlot.BackAttachment:
                SkinnedMeshRenderer newMeshBackAttachment = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshBackAttachment, 10, slotIndex, false);
                _magicArmorControl.BackAttachmentMagicArmor = item.MagicArmorModifier;
                EquipSlotAndIcon(11, item);
                break;
            case EquipmentSlot.Shoulders:
                SkinnedMeshRenderer newMeshShoulderLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshShoulderLeft, 12, slotIndex, false);
                SkinnedMeshRenderer newMeshShoulderRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshShoulderRight, 11, slotIndex, true);
                _armorControl.ShoulderLeftArmor = item.armorModifier;
                EquipSlotAndIcon(13, item);
                break;
            case EquipmentSlot.HeadSlot:

                if (item.hatNumber == 0)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringsBaseHair = Instantiate(item.Meshes[0]);
                    BoneTransformArmor(newMeshHeadCoveringsBaseHair, 13, slotIndex, false);
                }
                if (item.hatNumber == 1)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringNoHair = Instantiate(item.Meshes[0]);
                    BoneTransformArmor(newMeshHeadCoveringNoHair, 14, slotIndex, false);
                }
                if (item.hatNumber == 2)
                {
                    SkinnedMeshRenderer newMeshHeadNoElememts = Instantiate(item.Meshes[0]);
                    BoneTransformArmor(newMeshHeadNoElememts, 15, slotIndex, false);
                }
                EquipSlotAndIcon(0, item);
                _armorControl.HeadSlotArmor = item.armorModifier;
                break;
            case EquipmentSlot.Elbows:
                SkinnedMeshRenderer newMeshElbowLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshElbowLeft, 17, slotIndex, false);
                SkinnedMeshRenderer newMeshElbowRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshElbowRight, 16, slotIndex, true);
                _armorControl.ElbowLeftArmor = item.armorModifier;
                EquipSlotAndIcon(15, item);
                break;
            case EquipmentSlot.HipsAttachment:
                SkinnedMeshRenderer newMeshHipsAttachment = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshHipsAttachment, 18, slotIndex, false);
                break;
            case EquipmentSlot.Knees:
                SkinnedMeshRenderer newMeshKneeLeft = Instantiate(item.Meshes[0]);
                BoneTransformArmor(newMeshKneeLeft, 20, slotIndex, false);
                SkinnedMeshRenderer newMeshKneeRight = Instantiate(item.Meshes[1]);
                BoneTransformArmor(newMeshKneeRight, 19, slotIndex, true);
                _armorControl.KneeLeftArmor = item.armorModifier;
                EquipSlotAndIcon(18, item);
                break;
            case EquipmentSlot.WeaponTwoHand:
                CustomEvents.FireChangeIKHands(1);
                _weaponTimeCooldown.GreatSword();
                _anim.runtimeAnimatorController = Resources.Load("Animation/GreatSwordController") as RuntimeAnimatorController;
                GameObject newWeaponTwoHandPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponTwoHandPrefab, slotIndex, _weaponsAttachPoints[0], true);
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _armorControl.ShieldArmorPassive = 0;
                _equipSlot[21].Icon.gameObject.GetComponentInParent<Button>().enabled = false;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(19, item);
                EquipSlotAndIcon(21, item);
                _twoHandWeaponNow = true;
                break;
            case EquipmentSlot.WeaponRightHand:
                _twoHandWeaponNow = false;
                _weaponTimeCooldown.Sword();
                _anim.runtimeAnimatorController = Resources.Load("Animation/SwordController") as RuntimeAnimatorController;
                GameObject newWeaponRightHandPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponRightHandPrefab, slotIndex, _weaponsAttachPoints[1], true);
                _equipSlot[19].Icon.sprite = item.icon;
                _equipSlot[21].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                _twoHandWeaponNow = false;
                _equipSlot[20].EquipIcon();
                break;
            case EquipmentSlot.Shield:
                if (_twoHandWeaponNow == true)
                {
                    ResetAnimator();
                    _equipSlot[19].BackIcon.enabled = true;
                    _equipSlot[19].Icon.enabled = false;
                }
                Unequip(19);
                GameObject newShieldPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newShieldPrefab, slotIndex, _weaponsAttachPoints[2], false);
                _armorControl.ShieldArmorPassive = item.armorModifier;
                _armorControl.ShieldBlockArmorDefault = item.shieldBlockArmorModifier;
                _equipSlot[21].Icon.gameObject.GetComponentInParent<Button>().enabled = true;
                _shieldEquipSlotImage.color = new Color(_shieldEquipSlotImage.color.r, _shieldEquipSlotImage.color.g, _shieldEquipSlotImage.color.b, 1f);

                _twoHandWeaponNow = false;
                EquipSlotAndIcon(21, item);
                break;
            case EquipmentSlot.LeftRing:
                if (_magicArmorControl.RightRingMagicArmor == 0)
                {
                    EquipSlotAndIcon(23, item);
                    _magicArmorControl.RightRingMagicArmor = item.MagicArmorModifier;
                }
                else
                {
                    EquipSlotAndIcon(22, item);
                    _magicArmorControl.LeftRingMagicArmor = item.MagicArmorModifier;
                }
                break;
            case EquipmentSlot.LeftEarring:
                if (_magicArmorControl.RightEarringMagicArmor == 0)
                {
                    EquipSlotAndIcon(25, item);
                    _magicArmorControl.RightEarringMagicArmor = item.MagicArmorModifier;
                }
                else
                {
                    EquipSlotAndIcon(24, item);
                    _magicArmorControl.LeftEarringMagicArmor = item.MagicArmorModifier;
                }
                break;
            case EquipmentSlot.Necklace:
                EquipSlotAndIcon(26, item);
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
            _weaponTimeCooldown.NoWeapon();
            ResetAnimator();
        }
    }
}