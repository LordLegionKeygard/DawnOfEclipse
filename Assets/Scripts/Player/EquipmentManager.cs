using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    [SerializeField] private EquipSlot[] equipSlot;
    [SerializeField] private GameObject[] hairEarsHead;
    [SerializeField] private SkinnedMeshRenderer[] targetsMesh;
    [SerializeField] private Transform[] weaponsAttachPoints;
    private SkinnedMeshRenderer[] currentMeshes;
    private GameObject[] currentGameObject;
    public Equipment[] defaultEquipment;
    Equipment[] currentEquipment;
    [SerializeField] private Animator anim;
    [SerializeField] private Image shieldEquipSlotImage;
    [SerializeField] private ArmorControl armorControl;
    [SerializeField] private MeshFilter targetMeshFilterWeapon;
    [SerializeField] private MeshFilter targetMeshFilterShield;
    private WeaponTimeCooldown weaponTimeCooldown;
    [HideInInspector] public Button shieldButton;
    private bool twoHandWeaponNow;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    private Inventory inventory;

    private void Awake()
    {
        instance = this;
        weaponTimeCooldown = GetComponent<WeaponTimeCooldown>();
    }

    private void Start()
    {
        shieldButton = equipSlot[21].icon.gameObject.GetComponentInParent<Button>();
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        currentGameObject = new GameObject[numSlots];
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
            switch (newItem.noHair)
            {
                case 0: //withAll
                    ActiveAllHeadElements(0);
                    break;

                case 1:  //noHair & noEars
                    hairEarsHead[0].SetActive(false);
                    hairEarsHead[1].SetActive(false);
                    hairEarsHead[2].SetActive(true);
                    break;
                case 2: //fullHelmet
                    ActiveAllHeadElements(1);
                    break;
            }
        }
        if (newItem.weapon == true)
        {
            Equipment oldWeapon = Unequip(19);
            Equipment oldWeapon1 = Unequip(20);
            if (newItem.twoHandedWeapon == true)
            {
                twoHandWeaponNow = true;
                Equipment oldShield = Unequip(21);
            }
        }
        if (newItem.shield == true)
        {
            Equipment oldWeapon = Unequip(19);
        }
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    private void ActiveAllHeadElements(int active)
    {
        foreach (var headParts in hairEarsHead)
        {
            if (active == 0)
                headParts.SetActive(true);
            else
                headParts.SetActive(false);
        }
    }

    public void UnequipTwoHandedWeaponFromShield()
    {
        shieldButton.enabled = true;
        equipSlot[21].backIcon.enabled = true;
        equipSlot[21].icon.enabled = false;
    }
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            if (slotIndex == 0) { ActiveAllHeadElements(0); }

            if (slotIndex == 19 || slotIndex == 20)
            {
                weaponTimeCooldown.NoWeapon();
                if (slotIndex == 19)
                {
                    twoHandWeaponNow = false;
                    equipSlot[21].backIcon.enabled = true;
                    equipSlot[21].icon.enabled = false;
                }
            }

            if (currentMeshes[slotIndex] != null) { Destroy(currentMeshes[slotIndex].gameObject); }
            if (currentGameObject[slotIndex] != null) { Destroy(currentGameObject[slotIndex].gameObject); }

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null) { onEquipmentChanged.Invoke(null, oldItem); }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            ActiveAllHeadElements(0);
            Unequip(i);
        }
        armorControl.ResetArmor();
        EquipDefaults();
    }

    private void BoneTransformArmor(SkinnedMeshRenderer skin, int targetMeshNumber, int slot)
    {
        skin.transform.parent = targetsMesh[targetMeshNumber].transform.parent;
        skin.rootBone = targetsMesh[targetMeshNumber].rootBone;
        skin.bones = targetsMesh[targetMeshNumber].bones;
        currentMeshes[slot] = skin;
    }

    private void BoneTransformWeapon(GameObject weapon, int slot, Transform weaponPoint, bool rightHand)
    {
        if (rightHand) { weapon.transform.parent = targetMeshFilterWeapon.transform.parent; }
        else{ weapon.transform.parent = targetMeshFilterShield.transform.parent; }
        weapon.transform.position = weaponPoint.transform.position;
        weapon.transform.rotation = weaponPoint.transform.rotation;
        currentGameObject[slot] = weapon;
    }

    private void EquipSlotAndIcon(int equipSlotNumber, Equipment equipItem)
    {
        if (equipItem.icon != null)
        {
            equipSlot[equipSlotNumber].icon.sprite = equipItem.icon;
            equipSlot[equipSlotNumber].Icon();
        }
    }

    private void AttachToMesh(Equipment item, int slotIndex)
    {
        switch (item.equipSlot)
        {
            case EquipmentSlot.Torso:
                SkinnedMeshRenderer newMeshTorso = Instantiate(item.mesh);
                BoneTransformArmor(newMeshTorso, 0, slotIndex);
                armorControl.torsoArmor = item.armorModifier;
                EquipSlotAndIcon(1, item);
                break;
            case EquipmentSlot.HandRight:
                SkinnedMeshRenderer newMeshHandRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHandRight, 1, slotIndex);
                armorControl.handRightArmor = item.armorModifier;
                EquipSlotAndIcon(5, item);
                break;
            case EquipmentSlot.HandLeft:
                SkinnedMeshRenderer newMeshHandLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHandLeft, 2, slotIndex);
                armorControl.handLeftArmor = item.armorModifier;
                EquipSlotAndIcon(6, item);
                break;
            case EquipmentSlot.ArmUpperRight:
                SkinnedMeshRenderer newMeshArmUpperRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmUpperRight, 3, slotIndex);
                armorControl.armUpperRightArmor = item.armorModifier;
                EquipSlotAndIcon(7, item);
                break;
            case EquipmentSlot.ArmUpperLeft:
                SkinnedMeshRenderer newMeshArmUpperLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmUpperLeft, 4, slotIndex);
                armorControl.armUpperLeftArmor = item.armorModifier;
                EquipSlotAndIcon(8, item);
                break;
            case EquipmentSlot.ArmLowerRight:
                SkinnedMeshRenderer newMeshArmLowerRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmLowerRight, 5, slotIndex);
                armorControl.armLowerRightArmor = item.armorModifier;
                EquipSlotAndIcon(9, item);
                break;
            case EquipmentSlot.ArmLowerLeft:
                SkinnedMeshRenderer newMeshArmLowerLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshArmLowerLeft, 6, slotIndex);
                armorControl.armLowerLeftArmor = item.armorModifier;
                EquipSlotAndIcon(10, item);
                break;
            case EquipmentSlot.Hips:
                SkinnedMeshRenderer newMeshHips = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHips, 7, slotIndex);
                armorControl.hipsArmor = item.armorModifier;
                EquipSlotAndIcon(2, item);
                break;
            case EquipmentSlot.LegRight:
                SkinnedMeshRenderer newMeshLegRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshLegRight, 8, slotIndex);
                armorControl.legRightArmor = item.armorModifier;
                EquipSlotAndIcon(3, item);
                break;
            case EquipmentSlot.LegLeft:
                SkinnedMeshRenderer newMeshLegLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshLegLeft, 9, slotIndex);
                armorControl.legLeftArmor = item.armorModifier;
                EquipSlotAndIcon(4, item);
                break;

            case EquipmentSlot.BackAttachment:
                SkinnedMeshRenderer newMeshBackAttachment = Instantiate(item.mesh);
                BoneTransformArmor(newMeshBackAttachment, 10, slotIndex);
                armorControl.backAttachmentArmor = item.armorModifier;
                EquipSlotAndIcon(11, item);
                break;
            case EquipmentSlot.ShoulderRight:
                SkinnedMeshRenderer newMeshShoulderRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshShoulderRight, 11, slotIndex);
                armorControl.shoulderRightArmor = item.armorModifier;
                EquipSlotAndIcon(12, item);
                break;
            case EquipmentSlot.ShoulderLeft:
                SkinnedMeshRenderer newMeshShoulderLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshShoulderLeft, 12, slotIndex);
                armorControl.shoulderLeftArmor = item.armorModifier;
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
                armorControl.headSlotArmor = item.armorModifier;
                break;
            case EquipmentSlot.ElbowRight:
                SkinnedMeshRenderer newMeshElbowRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshElbowRight, 16, slotIndex);
                armorControl.elbowRightArmor = item.armorModifier;
                EquipSlotAndIcon(14, item);
                break;
            case EquipmentSlot.ElbowLeft:
                SkinnedMeshRenderer newMeshElbowLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshElbowLeft, 17, slotIndex);
                armorControl.elbowLeftArmor = item.armorModifier;
                EquipSlotAndIcon(15, item);
                break;
            case EquipmentSlot.HipsAttachment:
                SkinnedMeshRenderer newMeshHipsAttachment = Instantiate(item.mesh);
                BoneTransformArmor(newMeshHipsAttachment, 18, slotIndex);
                break;
            case EquipmentSlot.KneeRight:
                SkinnedMeshRenderer newMeshKneeRight = Instantiate(item.mesh);
                BoneTransformArmor(newMeshKneeRight, 19, slotIndex);
                armorControl.kneeRightArmor = item.armorModifier;
                EquipSlotAndIcon(17, item);
                break;
            case EquipmentSlot.KneeLeft:
                SkinnedMeshRenderer newMeshKneeLeft = Instantiate(item.mesh);
                BoneTransformArmor(newMeshKneeLeft, 20, slotIndex);
                armorControl.kneeLeftArmor = item.armorModifier;
                EquipSlotAndIcon(18, item);
                break;
            case EquipmentSlot.WeaponTwoHand:
                weaponTimeCooldown.GreatSword();
                anim.runtimeAnimatorController = Resources.Load("Animation/GreatSwordController") as RuntimeAnimatorController;
                GameObject newWeaponTwoHandPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponTwoHandPrefab, slotIndex, weaponsAttachPoints[0], true);
                armorControl.shieldBlockArmorDefault = item.shieldBlockArmorModifier;
                armorControl.shieldArmorPassive = 0;
                equipSlot[21].icon.gameObject.GetComponentInParent<Button>().enabled = false;
                shieldEquipSlotImage.color = new Color(shieldEquipSlotImage.color.r, shieldEquipSlotImage.color.g, shieldEquipSlotImage.color.b, 0.5f);
                EquipSlotAndIcon(19, item);
                EquipSlotAndIcon(21, item);
                twoHandWeaponNow = true;
                break;
            case EquipmentSlot.WeaponRightHand:
                weaponTimeCooldown.LongSword();
                anim.runtimeAnimatorController = Resources.Load("Animation/SwordAndShieldController") as RuntimeAnimatorController;
                GameObject newWeaponRightHandPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newWeaponRightHandPrefab, slotIndex, weaponsAttachPoints[1], true);
                equipSlot[19].icon.sprite = item.icon;
                equipSlot[21].icon.gameObject.GetComponentInParent<Button>().enabled = true;
                twoHandWeaponNow = false;
                equipSlot[20].Icon();
                break;
            case EquipmentSlot.Shield:
                GameObject newShieldPrefab = Instantiate(item.prefab);
                BoneTransformWeapon(newShieldPrefab, slotIndex, weaponsAttachPoints[2], false);
                armorControl.shieldArmorPassive = item.armorModifier;
                armorControl.shieldBlockArmorDefault = item.shieldBlockArmorModifier;
                equipSlot[21].icon.gameObject.GetComponentInParent<Button>().enabled = true;
                shieldEquipSlotImage.color = new Color(shieldEquipSlotImage.color.r, shieldEquipSlotImage.color.g, shieldEquipSlotImage.color.b, 1f);
                if (twoHandWeaponNow == true)
                {
                    anim.runtimeAnimatorController = Resources.Load("Animation/MainController") as RuntimeAnimatorController;
                    ResetAnimator();
                    equipSlot[19].backIcon.enabled = true;
                    equipSlot[19].icon.enabled = false;
                }
                twoHandWeaponNow = false;
                EquipSlotAndIcon(21, item);
                break;
        }
        armorControl.UpdateArmor();
    }

    public void ResetAnimator()
    {
        anim.runtimeAnimatorController = Resources.Load("Animation/MainController") as RuntimeAnimatorController;
    }

    private void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
        {
            Equip(e);
            weaponTimeCooldown.NoWeapon();
            ResetAnimator();
        }
    }
}