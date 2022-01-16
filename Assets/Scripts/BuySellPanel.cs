using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellPanel : MonoBehaviour
{
    [SerializeField] private GameObject _sellPanel;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private Button _sellPanelButton;
    [SerializeField] private Button _buyPanelButton;
    [SerializeField] private SelectSlot _selectSlot;


    public void SellButtonPanel()
    {
        _sellPanel.SetActive(true);
        _sellPanelButton.interactable = false;
        _buyPanelButton.interactable = true;
        _selectSlot.ClearSlot();
    }

    public void BuyButtonPanel()
    {
        _sellPanel.SetActive(false);
        _buyPanel.SetActive(true);
        _sellPanelButton.interactable = true;
        _buyPanelButton.interactable = false;
        _selectSlot.ClearSlot();
    }
}
