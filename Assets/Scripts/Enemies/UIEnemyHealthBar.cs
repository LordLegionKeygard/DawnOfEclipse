using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    Slider slider;
    float timeUntilBarIsHidde = 0;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        timeUntilBarIsHidde = 3;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void Update()
    {
        timeUntilBarIsHidde = timeUntilBarIsHidde - Time.deltaTime;

        if (slider != null)
        {
            if (timeUntilBarIsHidde <= 0)
            {
                timeUntilBarIsHidde = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }

            if (slider.value <= 0)
            {
                Destroy(slider.gameObject);
            }
        }
    }
}
