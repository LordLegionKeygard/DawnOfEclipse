using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private TextMeshProUGUI[] _inventoryText;

    private void Start()
    {
        _inventoryText[0].text = Language.Menu_text[1];
        _inventoryText[1].text = Language.Menu_text[2];
        _inventoryText[2].text = Language.Menu_text[3];
        _inventoryText[3].text = Language.Menu_text[4];
        _inventoryText[4].text = Language.Menu_text[5];
    }
}
