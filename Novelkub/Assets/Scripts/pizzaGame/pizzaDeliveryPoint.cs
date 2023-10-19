using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizzaDeliveryPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.pointCount++;
            gameObject.SetActive(false);
        }
    }
}