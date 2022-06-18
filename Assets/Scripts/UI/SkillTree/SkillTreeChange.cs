using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeChange : MonoBehaviour
{
    [SerializeField] private GameObject[] _skillTrees;
    [SerializeField] private Image[] _frameImages;
    [SerializeField] private Color[] _treeColors;

    public void ChangeTree(int number)
    {
        for (int i = 0; i < _skillTrees.Length; i++)
        {
            if (_skillTrees[i].activeInHierarchy)
            {
                _skillTrees[i].SetActive(false);

                if (i == _skillTrees.Length - 1 && number == 1)
                {
                    _skillTrees[0].SetActive(true);
                    ChangeColor(0);
                }
                else if (i == 0 && number == -1)
                {
                    _skillTrees[_skillTrees.Length - 1].SetActive(true);
                    ChangeColor(_skillTrees.Length - 1);
                }
                else
                {
                    _skillTrees[i + number].SetActive(true);
                    ChangeColor(i + number);
                }
                return;
            }          
        }
    }

    private void ChangeColor(int number)
    {
        foreach (var images in _frameImages)
        {
            images.color = _treeColors[number];
        }
    }
}
