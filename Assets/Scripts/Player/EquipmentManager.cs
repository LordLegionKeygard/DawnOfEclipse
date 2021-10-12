using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has functions for adding and removing items. */

public class EquipmentManager : MonoBehaviour
{

    #region Singleton

    public static EquipmentManager instance;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject hair;
    [SerializeField] private GameObject ears;
    [SerializeField] private GameObject head;
    [SerializeField] private Equipment[] defaultEquipment;
    [SerializeField] private SkinnedMeshRenderer targetMeshTorso;
    [SerializeField] private SkinnedMeshRenderer targetMeshRightHand;
    [SerializeField] private SkinnedMeshRenderer targetMeshLeftHand;
    [SerializeField] private SkinnedMeshRenderer targetMeshArmUpperRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshArmUpperLeft;
    [SerializeField] private SkinnedMeshRenderer targetMeshArmLowerRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshArmLowerLeft;
    [SerializeField] private SkinnedMeshRenderer targetMeshHips;
    [SerializeField] private SkinnedMeshRenderer targetMeshLegRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshLegLeft;
    [SerializeField] private SkinnedMeshRenderer targetMeshBackAttachmnet;
    [SerializeField] private SkinnedMeshRenderer targetMeshShoulderRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshShoulderLeft;
    [SerializeField] private SkinnedMeshRenderer targetMeshHeadCoveringsBaseHair;
    [SerializeField] private SkinnedMeshRenderer targetMeshHeadCoveringsNoHair;
    [SerializeField] private SkinnedMeshRenderer targetMeshHeadNoElements;
    [SerializeField] private SkinnedMeshRenderer targetMeshElbowRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshElbowLeft;
    [SerializeField] private SkinnedMeshRenderer targetMeshHipsAttachment;
    [SerializeField] private SkinnedMeshRenderer targetMeshKneeAttachmentRight;
    [SerializeField] private SkinnedMeshRenderer targetMeshKneeAttachmentLeft;
    [SerializeField] private MeshFilter _targetMeshFilterWeapon;
    [SerializeField] private Transform weaponPoint;
    SkinnedMeshRenderer[] currentMeshes;
    GameObject[] currentGameObject;
    private WeaponTimeCooldown weaponTimeCooldown;

    void Awake()
    {
        instance = this;
        weaponTimeCooldown = GetComponent<WeaponTimeCooldown>();
    }

    #endregion

    Equipment[] currentEquipment;   // Items we currently have equipped

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    Inventory inventory;    // Reference to our inventory

    void Start()
    {
        inventory = Inventory.instance;     // Get a reference to our inventory

        // Initialize currentEquipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        currentGameObject = new GameObject[numSlots];

        EquipDefaults();
    }

    // Equip a new item
    public void Equip(Equipment newItem)
    {
        if (newItem.canChangehead)
        {
            switch (newItem.noHair)
            {
                case 0: //withAll
                    head.SetActive(true);
                    ears.SetActive(true);
                    hair.SetActive(true);
                    break;

                case 1:  //noHair & noEars
                    ears.SetActive(false);
                    hair.SetActive(false);
                    head.SetActive(true);
                    break;
                case 2: //fullHelmet(noAll)
                    head.SetActive(false);
                    ears.SetActive(false);
                    hair.SetActive(false);
                    break;
            }
        }
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    // Unequip an item with a particular index
    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;
        // Only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // Add the item to the inventory
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            if (currentGameObject[slotIndex] != null)
            {
                Destroy(currentGameObject[slotIndex].gameObject);
            }

            // Remove the item from the equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            head.SetActive(true);
            ears.SetActive(true);
            hair.SetActive(true);
            Unequip(i);
        }

        EquipDefaults();
    }

    void AttachToMesh(Equipment item, int slotIndex)
    {
        switch (item.equipSlot)
        {
            case EquipmentSlot.Torso:
                SkinnedMeshRenderer newMeshTorso = Instantiate(item.mesh);
                newMeshTorso.transform.parent = targetMeshTorso.transform.parent;
                newMeshTorso.rootBone = targetMeshTorso.rootBone;
                newMeshTorso.bones = targetMeshTorso.bones;
                currentMeshes[slotIndex] = newMeshTorso;
                break;
            case EquipmentSlot.HandRight:
                SkinnedMeshRenderer newMeshHandRight = Instantiate(item.mesh);
                newMeshHandRight.transform.parent = targetMeshRightHand.transform.parent;
                newMeshHandRight.rootBone = targetMeshRightHand.rootBone;
                newMeshHandRight.bones = targetMeshRightHand.bones;
                currentMeshes[slotIndex] = newMeshHandRight;
                break;
            case EquipmentSlot.HandLeft:
                SkinnedMeshRenderer newMeshHandLeft = Instantiate(item.mesh);
                newMeshHandLeft.transform.parent = targetMeshLeftHand.transform.parent;
                newMeshHandLeft.rootBone = targetMeshLeftHand.rootBone;
                newMeshHandLeft.bones = targetMeshLeftHand.bones;
                currentMeshes[slotIndex] = newMeshHandLeft;
                break;
            case EquipmentSlot.ArmUpperRight:
                SkinnedMeshRenderer newMeshArmUpperRight = Instantiate(item.mesh);
                newMeshArmUpperRight.transform.parent = targetMeshArmUpperRight.transform.parent;
                newMeshArmUpperRight.rootBone = targetMeshArmUpperRight.rootBone;
                newMeshArmUpperRight.bones = targetMeshArmUpperRight.bones;
                currentMeshes[slotIndex] = newMeshArmUpperRight;
                break;
            case EquipmentSlot.ArmUpperLeft:
                SkinnedMeshRenderer newMeshArmUpperLeft = Instantiate(item.mesh);
                newMeshArmUpperLeft.transform.parent = targetMeshArmUpperLeft.transform.parent;
                newMeshArmUpperLeft.rootBone = targetMeshArmUpperLeft.rootBone;
                newMeshArmUpperLeft.bones = targetMeshArmUpperLeft.bones;
                currentMeshes[slotIndex] = newMeshArmUpperLeft;
                break;
            case EquipmentSlot.ArmLowerRight:
                SkinnedMeshRenderer newMeshArmLowerRight = Instantiate(item.mesh);
                newMeshArmLowerRight.transform.parent = targetMeshArmLowerRight.transform.parent;
                newMeshArmLowerRight.rootBone = targetMeshArmLowerRight.rootBone;
                newMeshArmLowerRight.bones = targetMeshArmLowerRight.bones;
                currentMeshes[slotIndex] = newMeshArmLowerRight;
                break;
            case EquipmentSlot.ArmLowerLeft:
                SkinnedMeshRenderer newMeshArmLowerLeft = Instantiate(item.mesh);
                newMeshArmLowerLeft.transform.parent = targetMeshArmLowerLeft.transform.parent;
                newMeshArmLowerLeft.rootBone = targetMeshArmLowerLeft.rootBone;
                newMeshArmLowerLeft.bones = targetMeshArmLowerLeft.bones;
                currentMeshes[slotIndex] = newMeshArmLowerLeft;
                break;
            case EquipmentSlot.Hips:
                SkinnedMeshRenderer newMeshHips = Instantiate(item.mesh);
                newMeshHips.transform.parent = targetMeshHips.transform.parent;
                newMeshHips.rootBone = targetMeshHips.rootBone;
                newMeshHips.bones = targetMeshHips.bones;
                currentMeshes[slotIndex] = newMeshHips;
                break;
            case EquipmentSlot.LegLeft:
                SkinnedMeshRenderer newMeshLegLeft = Instantiate(item.mesh);
                newMeshLegLeft.transform.parent = targetMeshLegLeft.transform.parent;
                newMeshLegLeft.rootBone = targetMeshLegLeft.rootBone;
                newMeshLegLeft.bones = targetMeshLegLeft.bones;
                currentMeshes[slotIndex] = newMeshLegLeft;
                break;
            case EquipmentSlot.LegRight:
                SkinnedMeshRenderer newMeshLegRight = Instantiate(item.mesh);
                newMeshLegRight.transform.parent = targetMeshLegRight.transform.parent;
                newMeshLegRight.rootBone = targetMeshLegRight.rootBone;
                newMeshLegRight.bones = targetMeshLegRight.bones;
                currentMeshes[slotIndex] = newMeshLegRight;
                break;
            case EquipmentSlot.BackAttachment:
                SkinnedMeshRenderer newMeshBackAttachment = Instantiate(item.mesh);
                newMeshBackAttachment.transform.parent = targetMeshBackAttachmnet.transform.parent;
                newMeshBackAttachment.rootBone = targetMeshBackAttachmnet.rootBone;
                newMeshBackAttachment.bones = targetMeshBackAttachmnet.bones;
                currentMeshes[slotIndex] = newMeshBackAttachment;
                break;
            case EquipmentSlot.ShoulderLeft:
                SkinnedMeshRenderer newMeshShoulderLeft = Instantiate(item.mesh);
                newMeshShoulderLeft.transform.parent = targetMeshShoulderLeft.transform.parent;
                newMeshShoulderLeft.rootBone = targetMeshShoulderLeft.rootBone;
                newMeshShoulderLeft.bones = targetMeshShoulderLeft.bones;
                currentMeshes[slotIndex] = newMeshShoulderLeft;
                break;
            case EquipmentSlot.ShoulderRight:
                SkinnedMeshRenderer newMeshShoulderRight = Instantiate(item.mesh);
                newMeshShoulderRight.transform.parent = targetMeshShoulderRight.transform.parent;
                newMeshShoulderRight.rootBone = targetMeshShoulderRight.rootBone;
                newMeshShoulderRight.bones = targetMeshShoulderRight.bones;
                currentMeshes[slotIndex] = newMeshShoulderRight;
                break;
            case EquipmentSlot.HeadSlot:

                if (item.hatNumber == 0)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringsBaseHair = Instantiate(item.mesh);
                    newMeshHeadCoveringsBaseHair.transform.parent = targetMeshHeadCoveringsBaseHair.transform.parent;
                    newMeshHeadCoveringsBaseHair.rootBone = targetMeshHeadCoveringsBaseHair.rootBone;
                    newMeshHeadCoveringsBaseHair.bones = targetMeshHeadCoveringsBaseHair.bones;
                    currentMeshes[slotIndex] = newMeshHeadCoveringsBaseHair;
                }
                if (item.hatNumber == 1)
                {
                    SkinnedMeshRenderer newMeshHeadCoveringNoHair = Instantiate(item.mesh);
                    newMeshHeadCoveringNoHair.transform.parent = targetMeshHeadCoveringsNoHair.transform.parent;
                    newMeshHeadCoveringNoHair.rootBone = targetMeshHeadCoveringsNoHair.rootBone;
                    newMeshHeadCoveringNoHair.bones = targetMeshHeadCoveringsNoHair.bones;
                    currentMeshes[slotIndex] = newMeshHeadCoveringNoHair;
                }
                if (item.hatNumber == 2)
                {
                    SkinnedMeshRenderer newMeshHeadNoElememts = Instantiate(item.mesh);
                    newMeshHeadNoElememts.transform.parent = targetMeshHeadNoElements.transform.parent;
                    newMeshHeadNoElememts.rootBone = targetMeshHeadNoElements.rootBone;
                    newMeshHeadNoElememts.bones = targetMeshHeadNoElements.bones;
                    currentMeshes[slotIndex] = newMeshHeadNoElememts;
                }
                break;
            case EquipmentSlot.ElbowRight:
                SkinnedMeshRenderer newMeshElbowRight = Instantiate(item.mesh);
                newMeshElbowRight.transform.parent = targetMeshElbowRight.transform.parent;
                newMeshElbowRight.rootBone = targetMeshElbowRight.rootBone;
                newMeshElbowRight.bones = targetMeshElbowRight.bones;
                currentMeshes[slotIndex] = newMeshElbowRight;
                break;
            case EquipmentSlot.ElbowLeft:
                SkinnedMeshRenderer newMeshElbowLeft = Instantiate(item.mesh);
                newMeshElbowLeft.transform.parent = targetMeshElbowLeft.transform.parent;
                newMeshElbowLeft.rootBone = targetMeshElbowLeft.rootBone;
                newMeshElbowLeft.bones = targetMeshElbowLeft.bones;
                currentMeshes[slotIndex] = newMeshElbowLeft;
                break;
            case EquipmentSlot.HipsAttachment:
                SkinnedMeshRenderer newMeshHipsAttachment = Instantiate(item.mesh);
                newMeshHipsAttachment.transform.parent = targetMeshHipsAttachment.transform.parent;
                newMeshHipsAttachment.rootBone = targetMeshHipsAttachment.rootBone;
                newMeshHipsAttachment.bones = targetMeshHipsAttachment.bones;
                currentMeshes[slotIndex] = newMeshHipsAttachment;
                break;
            case EquipmentSlot.KneeRight:
                SkinnedMeshRenderer newMeshKneeRight = Instantiate(item.mesh);
                newMeshKneeRight.transform.parent = targetMeshKneeAttachmentRight.transform.parent;
                newMeshKneeRight.rootBone = targetMeshKneeAttachmentRight.rootBone;
                newMeshKneeRight.bones = targetMeshKneeAttachmentRight.bones;
                currentMeshes[slotIndex] = newMeshKneeRight;
                break;
            case EquipmentSlot.KneeLeft:
                SkinnedMeshRenderer newMeshKneeLeft = Instantiate(item.mesh);
                newMeshKneeLeft.transform.parent = targetMeshKneeAttachmentLeft.transform.parent;
                newMeshKneeLeft.rootBone = targetMeshKneeAttachmentLeft.rootBone;
                newMeshKneeLeft.bones = targetMeshKneeAttachmentLeft.bones;
                currentMeshes[slotIndex] = newMeshKneeLeft;
                break;
            case EquipmentSlot.WeaponTwoHand:
                weaponTimeCooldown.GreatSword();
                anim.runtimeAnimatorController = Resources.Load("Animation/GreatSwordController") as RuntimeAnimatorController;
                GameObject newGameObhectPrefab = Instantiate(item.prefab);
                newGameObhectPrefab.transform.parent = _targetMeshFilterWeapon.transform.parent;
                newGameObhectPrefab.transform.position = weaponPoint.transform.position;
                newGameObhectPrefab.transform.rotation = weaponPoint.transform.rotation;
                currentGameObject[slotIndex] = newGameObhectPrefab;
                break;
        }
    }

    void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
        {
            Equip(e);
            weaponTimeCooldown.NoWeapon();
            anim.runtimeAnimatorController = Resources.Load("Animation/MainController") as RuntimeAnimatorController;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

}
