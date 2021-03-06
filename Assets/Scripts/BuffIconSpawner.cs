using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _buffIconsPanel;
    [SerializeField] private GameObject _buffIconsPrefab;
    public void SpawnBuffIcon(SkillInfo skillInfo)
    {
        var buffIcon = Instantiate(_buffIconsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buffIcon.transform.SetParent(_buffIconsPanel.transform);
        var BuffIcon = buffIcon.GetComponent<BuffIcon>();
        BuffIcon.BackIcon.sprite = skillInfo.SkillIcon;
        BuffIcon.BuffCooldown = skillInfo.BuffCooldown;
        BuffIcon.IconBuffNumber = skillInfo.BuffNumber;
    }

    public void SpawnBuffIcon(Sprite sprite, int buffCooldown, int iconBuffNumber)
    {
        var buffIcon = Instantiate(_buffIconsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buffIcon.transform.SetParent(_buffIconsPanel.transform);
        var BuffIcon = buffIcon.GetComponent<BuffIcon>();
        BuffIcon.BackIcon.sprite = sprite;
        BuffIcon.BuffCooldown = buffCooldown;
        BuffIcon.IconBuffNumber = iconBuffNumber;

        BuffIcon.ForeIcon.fillMethod = Image.FillMethod.Vertical;
    }
}
