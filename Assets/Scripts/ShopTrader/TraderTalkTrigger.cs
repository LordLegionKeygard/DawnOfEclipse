using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderTalkTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _traderShopPanel;
    private TraderItems _traderItems;

    private void Start()
    {
        _traderItems = GetComponentInParent<TraderItems>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            _traderItems.AddItemToShop();
            _traderShopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _traderShopPanel.SetActive(false);
        }
    }
}
