using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    public Image BuffImage;

    public float BuffCooldown;

    private void Start()
    {   
        Destroy(gameObject, BuffCooldown);
    }
}
