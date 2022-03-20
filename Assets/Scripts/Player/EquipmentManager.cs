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
    private SkinnedMeshRenderer[] _currentMeshes;
    private GameObject[] _currentGameObject;
    public Equipment[] _currentEquipment;
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
                    ActiveAllHeadElements(0);
                    break;

                case 1:  //noHair & noEars
                    _hairEarsHead[0].SetActive(false);
                    _hairEarsHead[1].SetActive(false);
                    _hairEarsHead[2].SetActive(true);
                    break;
                case 2: //fullHelmet
                    ActiveAllHeadElements(1);
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

        Equipment oldItem = Unequip(slotIndex);

        if (_onEquipmentChanged != null)
        {
            _onEquipmentChanged.Invoke(newItem, oldItem);
        }

        _currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    public void ActiveAllHeadElements(int active)
    {
        foreach (var headParts in _hairEarsHead)
        {
            if (active == 0)
                headParts.SetActive(true);
            else
                headParts.SetActive(false);
        }
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
        Debug.Log("SlotIndex" + slotIndex);
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
                    _equipSlot[21].BackIcon.enabled = true;
                    _equipSlot[21].Icon.enabled = false;
                }
            }

            if (_currentMeshes[slotIndex] != null) { Destroy(_currentMeshes[slotIndex].gameObject); }
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
            ActiveAllHeadElements(0);
            Unequip(i);
        }
        _armorControl.ResetArmor();
        _magicArmorControl.ResetArmor();
        EquipDefaults();
    }

    private void BoneTransformArmor(SkinnedMeshRenderer skin, int targetMeshNumber, int slot)
    {
        skin.transform.parent = _targetsMesh[targetMeshNumber].transform.parent;
        skin.rootBone = _targetsMesh[targetMeshNumber].rootBone;
        skin.bones = _targetsMesh[targetMeshNumber].bones;
        _currentMeshes[slot] = skin;
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
        }
    }

    private void AttachToMesh(Equipment item, int slotIndex)
    {
        switch (item.equipSlot)
        {
            case EquipmentSlot.Torso:
                SkinnedMeshRenderer newMeshTorso = Instantiate(item.mesh);
                BoneTransformArmor(newMeshTorso, 0, slotIndex);
                _armorControl.TorsoArmor = item.armorModifier;
                EquipSlotAndIcon(1, item);
                break;
            case EquipmentSlot.HandRight:
                SkinnedMeshRenderer newMeshHandRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHandRight, 1, slotIndex);
                _armorControl.HandRightArmor = item.armorModifier;
                EquipSlotAndIcon(5, item);
                break;
            case EquipmentSlot.HandLeft:
                SkinnedMeshRenderer newMeshHandLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHandLeft, 2, slotIndex);
                _armorControl.HandLeftArmor = item.armorModifier;
                EquipSlotAndIcon(6, item);
                break;
            case EquipmentSlot.ArmUpperRight:
                SkinnedMeshRenderer newMeshArmUpperRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmUpperRight, 3, slotIndex);
                _armorControl.ArmUpperRightArmor = item.armorModifier;
                EquipSlotAndIcon(7, item);
                break;
            case EquipmentSlot.ArmUpperLeft:
                SkinnedMeshRenderer newMeshArmUpperLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmUpperLeft, 4, slotIndex);
                _armorControl.ArmUpperLeftArmor = item.armorModifier;
                EquipSlotAndIcon(8, item);
                break;
            case EquipmentSlot.ArmLowerRight:
                SkinnedMeshRenderer newMeshArmLowerRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmLowerRight, 5, slotIndex);
                _armorControl.ArmLowerRightArmor = item.armorModifier;
                EquipSlotAndIcon(9, item);
                break;
            case EquipmentSlot.ArmLowerLeft:
                SkinnedMeshRenderer newMeshArmLowerLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmLowerLeft, 6, slotIndex);
                _armorControl.ArmLowerLeftArmor = item.armorModifier;
                EquipSlotAndIcon(10, item);
                break;
            case EquipmentSlot.Hips:
                SkinnedMeshRenderer newMeshHips = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHips, 7, slotIndex);
                _armorControl.HipsArmor = item.armorModifier;
                EquipSlotAndIcon(2, item);
                break;
            case EquipmentSlot.LegRight:
                SkinnedMeshRenderer newMeshLegRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshLegRight, 8, slotIndex);
                _armorControl.LegRightArmor = item.armorModifier;
                EquipSlotAndIcon(3, item);
                break;
            case EquipmentSlot.LegLeft:
                SkinnedMeshRenderer newMeshLegLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshLegLeft, 9, slotIndex);
                _armorControl.LegLeftArmor = item.armorModifier;
                EquipSlotAndIcon(4, item);
                break;

            case EquipmentSlot.BackAttachment:
                SkinnedMeshRenderer newMeshBackAttachment = Instantiate(item.mesh);
                BoneTransformArmor(newMeshBackAttachment, 10, slotIndex);
                _armorControl.BackAttachmentArmor = item.armorModifier;
                EquipSlotAndIcon(11, item);
                break;
            case EquipmentSlot.ShoulderRight:
                SkinnedMeshRenderer newMeshShoulderRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshShoulderRight, 11, slotIndex);
                _armorControl.ShoulderRightArmor = item.armorModifier;
                EquipSlotAndIcon(12, item);
                break;
            case EquipmentSlot.ShoulderLeft:
                SkinnedMeshRenderer newMeshShoulderLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshShoulderLeft, 12, slotIndex);
                _armorControl.ShoulderLeftArmor = item.armorModifier;
                EquipSlotAndIcon(13, item);
                break;

            case EquipmentSlot.HeadSlot:

                if (item.hatNumber == 0)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringsBaseHair = Instantiate(item.mesh);
                    BoneTransformArmor(newMeshHeadCoveringsBaseHair, 13, slotIndex);
                }
                if (item.hatNumber == 1)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringNoHair = Instantiate(item.mesh);
                    BoneTransformArmor(newMeshHeadCoveringNoHair, 14, slotIndex);
                }
                if (item.hatNumber == 2)
                {
                    SkinnedMeshRenderer newMeshHeadNoElememts = Instantiate(item.mesh);
                    BoneTransformArmor(newMeshHeadNoElememts, 15, slotIndex);
                }
                EquipSlotAndIcon(0, item);
                _armorControl.HeadSlotArmor = item.armorModifier;
                break;
            case EquipmentSlot.ElbowRight:
                SkinnedMeshRenderer newMeshElbowRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshElbowRight, 16, slotIndex);
                _armorControl.ElbowRightArmor = item.armorModifier;
                EquipSlotAndIcon(14, item);
                break;
            case EquipmentSlot.ElbowLeft:
                SkinnedMeshRenderer newMeshElbowLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshElbowLeft, 17, slotIndex);
                _armorControl.ElbowLeftArmor = item.armorModifier;
                EquipSlotAndIcon(15, item);
                break;
            case EquipmentSlot.HipsAttachment:
                SkinnedMeshRenderer newMeshHipsAttachment = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHipsAttachment, 18, slotIndex);
                break;
            case EquipmentSlot.KneeRight:
                SkinnedMeshRenderer newMeshKneeRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshKneeRight, 19, slotIndex);
                _armorControl.KneeRightArmor = item.armorModifier;
                EquipSlotAndIcon(17, item);
                break;
            case EquipmentSlot.KneeLeft:
                SkinnedMeshRenderer newMeshKneeLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshKneeLeft, 20, slotIndex);
                _armorControl.KneeLeftArmor = item.armorModifier;
                EquipSlotAndIcon(18, item);
                break;
            case EquipmentSlot.WeaponTwoHand:
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
                _weaponTimeCooldown.LongSword();
                _anim.runtimeAnimatorController = Resources.Load("Animation/SwordAndShieldController") as RuntimeAnimatorController;
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
        }
        _armorControl.UpdateArmor();
        _magicArmorControl.UpdateMagicArmor();
    }

    public void ResetAnimator()
    {
        _anim.runtimeAnimatorController = Resources.Load("Animation/MainController") as RuntimeAnimatorController;
        Debug.Log("ResetAnimator");
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