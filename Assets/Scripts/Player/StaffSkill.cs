using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffSkill : MonoBehaviour
{
    [Header("R1")]
    [SerializeField] private GameObject _skillR1CastPrefab;
    [SerializeField] private GameObject _skillR1Prefab;
    [SerializeField] private Transform _castR1Point;
    [SerializeField] private Transform _skillR1Point;

    [Header("R2")]
    [SerializeField] private GameObject _skillR2CastPrefab;
    [SerializeField] private GameObject _skillR2Prefab;
    [SerializeField] private Transform _castR2Point;
    [SerializeField] private Transform _skillR2Point;

    [Header("Other")]

    [SerializeField] private SkillFirePoints _skillFirePoints;
    private void OnEnable()
    {
        CustomEvents.OnUseSkillR1 += SkillR1;
        CustomEvents.OnUseSkillR2 += SkillR2;
    }

    private void Start()
    {
        _skillFirePoints = GetComponentInParent<SkillFirePoints>();
        _castR2Point = _skillFirePoints.FirePoints[0];
        _skillR2Point = _skillFirePoints.FirePoints[1];
        _castR1Point = _skillFirePoints.FirePoints[2];
        // _skillR1Point = _skillFirePoints.FirePoints[3];
    }

    private void SkillR1(bool cast)
    {
        if (cast)
        {
            var puk = Instantiate(_skillR1CastPrefab, _castR1Point.transform.position, Quaternion.identity);
            puk.transform.SetParent(_castR1Point.transform);
        }
        else
        {
            Instantiate(_skillR1Prefab, _skillR1Point.transform.position, Quaternion.identity);

        }
    }

    private void SkillR2(bool cast)
    {
        if (cast) Instantiate(_skillR2CastPrefab, _castR2Point.transform.position, Quaternion.identity);
        else Instantiate(_skillR2Prefab, _skillR2Point.transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        CustomEvents.OnUseSkillR1 -= SkillR1;
        CustomEvents.OnUseSkillR2 -= SkillR2;
    }
}
