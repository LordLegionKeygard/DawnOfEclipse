using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private CharacterCreationZoomSystem _characterCreationZoomSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _panel;
    [SerializeField] private string _scene;

    public void Load()
    {
        _panel.SetActive(false);
        _characterCreationZoomSystem.CameraBack();
        _animator.SetTrigger("sit");
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
}
