using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    public Image BackIcon;
    public Image ForeIcon;
    private float _constant;
    public float BuffCooldown;
    public int IconBuffNumber;

    private void Start()
    {
        CustomEvents.OnCheckIdenticalBuff += DestroyIdenticalBuff;
        ForeIcon.sprite = BackIcon.sprite;
        _constant = 1 / BuffCooldown;
        Destroy(gameObject, BuffCooldown);
    }

    private void Update()
    {
        ForeIcon.fillAmount -= _constant * Time.deltaTime;
    }

    private void DestroyIdenticalBuff(int number)
    {
        if (IconBuffNumber == number) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CustomEvents.OnCheckIdenticalBuff -= DestroyIdenticalBuff;
    }
}
