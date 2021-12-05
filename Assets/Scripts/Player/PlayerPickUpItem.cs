using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("DropItem") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            other.gameObject.GetComponent<ItemPickup>().PickUp();
        }
        if(other.gameObject.CompareTag("Money") && (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E)))
        {
            other.gameObject.GetComponent<MoneyPickUp>().PickUp(other.gameObject.GetComponent<MoneyPickUp>().DropCoins);
        }
    }
}
