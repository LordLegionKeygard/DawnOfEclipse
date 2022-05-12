using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRaceInventory : MonoBehaviour
{
    [SerializeField] private Image[] _headImages;
    [SerializeField] private Image[] _legImages;
    [SerializeField] private Image[] _satyrImages;
    [SerializeField] private Image[] _mushroomImages;

    private void Start()
    {
        CheckRace();
    }

    private void CheckRace()
    {
        switch (CharacterInformation.Race)
        {
            case 0:
                ImagesToggle(_headImages, false);
                ImagesToggle(_legImages, false);
                ImagesToggle(_satyrImages, true);
                break;
            case 1:
                ImagesToggle(_headImages, false);
                ImagesToggle(_legImages, false);
                ImagesToggle(_mushroomImages, true);
                break;
        }
    }

    private void ImagesToggle(Image[] images, bool state)
    {
        foreach (var item in images)
        {
            item.enabled = state;
        }
    }
}
