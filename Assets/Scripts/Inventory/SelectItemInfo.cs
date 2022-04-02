using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectItemInfo : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI[] _itemText;
    [SerializeField] private GameObject[] _setEffect;

    public void UpdateItemInfoText(string name, string type, string info)
    {
        _itemText[0].text = name;
        _itemText[1].text = type;
        _itemText[2].text = info;
    }

    public void UpdateItemSetEffectInfoText(string helmet, string shoulders, string torso, string forearms, string elbows, string bracers, string gloves, string hips, string knees, string boots, string threePieces, string sixPieces, string eightPieces, string tenPieces)
    {
        ToggleSetEffect(true);
        _itemText[4].text = helmet;
        _itemText[5].text = shoulders;
        _itemText[6].text = torso;
        _itemText[7].text = forearms;
        _itemText[8].text = elbows;
        _itemText[9].text = bracers;
        _itemText[10].text = gloves;
        _itemText[11].text = hips;
        _itemText[12].text = knees;
        _itemText[13].text = boots;
        _itemText[14].text = threePieces;
        _itemText[15].text = sixPieces;
        _itemText[16].text = eightPieces;
        _itemText[17].text = tenPieces;

        CheckEmptyInfoText();
    }

    private void CheckEmptyInfoText()
    {
        Debug.Log("Check");
        for (int i = 0; i < _itemText.Length; i++)
        {
            if (_itemText[i].text == "")
            {
                _itemText[i].gameObject.SetActive(false);
                Debug.Log("false");
            }
            else
            {
                _itemText[i].gameObject.SetActive(true);
                Debug.Log("true");
            }
        }
    }

    public void ToggleSetEffect(bool state)
    {
        foreach (var item in _setEffect)
        {
            item.SetActive(state);
        }
    }

    public void UpdateTransform(Vector2 vec)
    {
        transform.position = new Vector2(vec.x - 50, vec.y - 50);
    }
}
