using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTraderShopUpdate : MonoBehaviour
{
    [SerializeField] private GameObject _buyItemParent;
    [SerializeField] private GameObject _sellItemParent;
    [SerializeField] private Button _buyPanel;
    [SerializeField] private Button _sellPanel;
    [SerializeField] private RectTransform[] _contents;
    private void OnEnable()
    {
        _buyPanel.interactable = false;
        _sellPanel.interactable = true;
        _buyItemParent.SetActive(true);
        _sellItemParent.SetActive(false);
        foreach (var item in _contents)
        {
            item.anchoredPosition = new Vector2(0,0);
        }
    }
}
