using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkinColor : MonoBehaviour
{
    [SerializeField] private Material _playerSkinMats;
    [SerializeField] private Material[] _mats;

    private void Awake()
    {
        foreach (var mats in _mats)
        {
            mats.SetColor("_Color_Skin", _playerSkinMats.GetColor("_Color_Skin"));
        }
    }
}
