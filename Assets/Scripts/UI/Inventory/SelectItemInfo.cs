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

    public void UpdateItemSetEffectInfoText(string helmet, string shoulders, string torso, string forearms, string elbows, string bracers, string gloves,
    string hips, string knees, string boots, string threePieces, string fourPieces, string fivePieces, string sixPieces, string sevenPieces, string eightPieces, string ninePieces,
    string tenPieces)
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
        _itemText[15].text = fourPieces;
        _itemText[16].text = fivePieces;
        _itemText[17].text = sixPieces;
        _itemText[18].text = sevenPieces;
        _itemText[19].text = eightPieces;
        _itemText[20].text = ninePieces;
        _itemText[21].text = tenPieces;

        CheckEmptyInfoText();
    }

    private void CheckEmptyInfoText()
    {
        for (int i = 0; i < _itemText.Length; i++)
        {
            if (_itemText[i].text == "")
            {
                _itemText[i].gameObject.SetActive(false);
            }
            else
            {
                _itemText[i].gameObject.SetActive(true);
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
        transform.position = new Vector2(vec.x, vec.y);
    }
}
