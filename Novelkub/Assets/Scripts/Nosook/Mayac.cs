using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayac : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mayacPos;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                rigidbody.GetComponent<Rigidbody>().useGravity = false;
                Debug.Log("나 들어갔어");
                transform.position = mayacPos.position;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            rigidbody.GetComponent<Rigidbody>().useGravity = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            Debug.Log("다음 퀘스트로 갈 준비가 되었습니다 isnextquest ture라고 해서 해보기");
        }
    }
}