using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab))
        {
            PanelsToggle(0);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            PanelsToggle(1);
        }
    }

    public void PanelsToggle(int number)
    {
        panels[number].SetActive(!panels[number].activeSelf);
    }
}
