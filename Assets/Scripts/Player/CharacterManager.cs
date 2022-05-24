using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock on Transform")]
    public Transform LockOnTransform;

    [Header("A.I Settings")]
    public static float MaximumDetectionAngle = 180;
    public static float MinimumDetectionAngle = -180;
    [HideInInspector] public float currentRecoveryTime = 0;
    
}
