using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock on Transform")]
    public Transform lockOnTransform;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
    public bool canRotate;

    [Header("A.I Settings")]
    public static float detectionRadius = 10;
    public static float maximumDetectionAngle = 180;
    public static float minimumDetectionAngle = -180;
    public float currentRecoveryTime = 0;
}
