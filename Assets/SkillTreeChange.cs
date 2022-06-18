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
        foreach (var tree in _skillTrees) tree.SetActive(false);

        _skillTrees[number].SetActive(true);

        foreach (var images in _frameImages)
        {
            images.color = _treeColors[number];
        }
    }
}
