using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private float _timeUntilBarIsHidde = 7;

    public void SetHealth(int health)
    {
        _slider.value = health;
        _timeUntilBarIsHidde = 7;
        _slider.gameObject.SetActive(true);
    }

    public void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }

    private void Update()
    {
        if(_slider == null || !_slider.gameObject.activeInHierarchy) return;
        _timeUntilBarIsHidde -= Time.deltaTime;

        if (_timeUntilBarIsHidde <= 0)
        {
            _timeUntilBarIsHidde = 0;
            _slider.gameObject.SetActive(false);
        }

        if (_slider.value <= 0)
        {
            _slider.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
