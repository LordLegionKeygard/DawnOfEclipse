using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private CharacterCreationZoomSystem _characterCreationZoomSystem;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private string _scene;

    private void OnEnable()
    {
        CustomEvents.OnCharacterCreate += Load;
    }

    public void Load()
    {
        foreach (var panel in _panels) panel.SetActive(false);
        
        _characterCreationZoomSystem.CameraBack();
        StartCoroutine(ExecuteAfterTime(5f));
        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _loadingScreen.SetActive(true);
        }
        StartCoroutine(ExecuteAfterTime1(6f));
        IEnumerator ExecuteAfterTime1(float timeInSec)
        {
            yield return new WaitForSeconds(timeInSec);
            _loadingScreen.SetActive(true);
            SceneManager.LoadScene(_scene);
        }
    }
    private void OnDisable()
    {
        CustomEvents.OnCharacterCreate -= Load;
    }
}
