using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Potal : MonoBehaviour
{
    public Player player;
    public GameObject PubPotal;
    public GameObject InPotal;
    private Collider collider;
    int i = -1;
    //public GameObject PubPotalIn;
    // Start is called before the first frame update
    void Start()
    {
        collider = InPotal.GetComponent<Collider>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void Teleport(Vector3 spawnPosition)
    //{
    //    Controller.enabled = false;
    //    transform.position = spawnPosition;
    //    Controller.enabled = true;
    //}
    //player.Teleport(PubPotal.transform.position);
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            player.Teleport(PubPotal.transform.position + new Vector3(0,2,i));
            Debug.Log("나 들어왔어");
            i = -i;
            //other.gameObject.transform.position = PubPotal.transform.position;
            //collider.enabled = false;

        }
    }
}
