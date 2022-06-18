using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSkillTreePoints : MonoBehaviour
{
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
        CustomEvents.OnTakeTome += TakeTome;
    }

    private void TakeTome(int tome)
    {
        switch (tome)
        {
            case 0:
                DarkPoints += 1;
                _pointsText[0].text = DarkPoints.ToString();
                break;
            case 1:
                FirePoints += 1;
                _pointsText[1].text = FirePoints.ToString();
                break;
            case 2:
                IcePoints += 1;
                _pointsText[2].text = IcePoints.ToString();
                break;
            case 3:
                LightPoints += 1;
                _pointsText[3].text = LightPoints.ToString();
                break;
            case 4:
                NaturePoints += 1;
                _pointsText[4].text = NaturePoints.ToString();
                break;
            case 5:
                StormPoints += 1;
                _pointsText[5].text = StormPoints.ToString();
                break;
            case 6:
                ArcanePoints += 1;
                _pointsText[6].text = ArcanePoints.ToString();
                break;
            case 7:
                BloodPoints += 1;
                _pointsText[7].text = BloodPoints.ToString();
                break;
        }
    }
    private void OnDisable()
    {
        CustomEvents.OnTakeTome -= TakeTome;
    }
}
