using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayac : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mayacPos;
    void Start()
    {

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
                transform.position = mayacPos.position;
            }

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