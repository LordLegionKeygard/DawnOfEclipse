using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock on Transform")]
    public Transform lockOnTransform;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
}
