using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Tab))
        {
            PanelsToggle(0);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PanelsToggle(1);
            CustomEvents.FireUpdateAllStats();
        }
    }

    public void PanelsToggle(int number)
    {
        _panels[number].SetActive(!_panels[number].activeSelf);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
