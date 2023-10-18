using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject pizzaOwner;
    public GameObject pizza;


    void Start()
    {
        InvokeRepeating("makePizza", 0.0f, 0.2f);
    }


    void Update()
    {
        float x = pizzaOwner.transform.position.x;
        float y = pizzaOwner.transform.position.y + 2.0f;
        Instantiate(pizza, new Vector3(x, y, 0), Quaternion.identity);
    }
}
