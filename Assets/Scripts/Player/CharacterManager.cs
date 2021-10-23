using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock on Transform")]
    [HideInInspector] public Transform lockOnTransform;

    [Header("Movement Flags")]
    [HideInInspector] public bool isRotatingWithRootMotion;
    [HideInInspector] public bool canRotate;

    [Header("A.I Settings")]
    public static float detectionRadius = 10;
    public static float maximumDetectionAngle = 180;
    public static float minimumDetectionAngle = -180;
    [HideInInspector] public float currentRecoveryTime = 0;
}
