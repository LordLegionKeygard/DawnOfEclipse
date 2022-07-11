using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSkillTreePoints : MonoBehaviour
{
    public static CharacterSkillTreePoints CharacterSkillTreePointsS;
    public int DarkPoints;
    public int FirePoints;
    public int IcePoints;
    public int LightPoints;
    public int NaturePoints;
    public int StormPoints;
    public int ArcanePoints;
    public int BloodPoints;
    [SerializeField] private TextMeshProUGUI[] _pointsText;

    private void OnEnable()
    {
        CustomEvents.OnChangeSkillTreePoints += ChangeSkillTreePoints;
    }

    private void Awake()
    {
        if(CharacterSkillTreePointsS != null)
        {
            Debug.LogWarning("More than one instance of CharacterSkillTreePoints found!");
            return;
        }
        else
        {
            CharacterSkillTreePointsS = this;
        }
    }

    public bool HavePoints(int number)
    {
        switch (number)
        {
            case 0:
                if (DarkPoints >= 1)
                {
                    return true;
                }
                break;

        }
        return false;
    }

    private void ChangeSkillTreePoints(int tome, int number)
    {
        switch (tome)
        {
            case 0:
                DarkPoints += number;
                _pointsText[0].text = DarkPoints.ToString();
                break;
            case 1:
                FirePoints += number;
                _pointsText[1].text = FirePoints.ToString();
                break;
            case 2:
                IcePoints += number;
                _pointsText[2].text = IcePoints.ToString();
                break;
            case 3:
                LightPoints += number;
                _pointsText[3].text = LightPoints.ToString();
                break;
            case 4:
                NaturePoints += number;
                _pointsText[4].text = NaturePoints.ToString();
                break;
            case 5:
                StormPoints += number;
                _pointsText[5].text = StormPoints.ToString();
                break;
            case 6:
                ArcanePoints += number;
                _pointsText[6].text = ArcanePoints.ToString();
                break;
            case 7:
                BloodPoints += number;
                _pointsText[7].text = BloodPoints.ToString();
                break;
        }
    }
    private void OnDisable()
    {
        CustomEvents.OnChangeSkillTreePoints -= ChangeSkillTreePoints;
    }
}
