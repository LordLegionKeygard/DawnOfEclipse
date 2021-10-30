using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;

    private int maxStamina = 1000;

    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.0001f);

    private Coroutine regen;

    public int CurrentStamina => currentStamina;

    public bool staminaRun = false;
    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    private void FixedUpdate()
    {
        if (currentStamina > 10 && staminaRun)
        {
            currentStamina -= 2;
            staminaBar.value = currentStamina;
            RegenTimer();
        }
    }

    private void Update()
    {
        if (staminaBar.value == currentStamina)
        {
            return;
        }
        if (staminaBar.value > currentStamina)
        {
            staminaBar.value -= Time.deltaTime * 400;
        }
        if (staminaBar.value < currentStamina)
        {
            staminaBar.value += Time.deltaTime * 400;
        }

    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            RegenTimer();
        }
    }

    private void RegenTimer()
    {
        if (regen != null)
            StopCoroutine(regen);

        regen = StartCoroutine(RegenStamina());
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1.5f);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 1000;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
