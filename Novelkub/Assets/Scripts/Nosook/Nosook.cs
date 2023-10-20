using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Nosook : MonoBehaviour
{
    public GameObject mySelf;
    public Transform NosookPosition;
    public bool isMove;
    Animator animator;
    private Collider collider;
    public GameObject InPotal;
    // Start is called before the first frame update
    void Start()
    {
        collider = InPotal.GetComponent<Collider>();
        // mySelf.transform.position = Vector3.MoveTowards(start, nextPosition, 1);
        animator = GetComponentInChildren<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        MoveOn();


    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isMoving();
        }
    }

    public void OnPotal()
    {
        collider.enabled = true;
    }
    void MoveOn()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, NosookPosition.position, 4 * Time.deltaTime);
            animator.SetBool("Run", true);
            if (transform.position == NosookPosition.position)
            {
                animator.SetBool("Idle", true);
                OnPotal();
                SpwanManager.Instance.OnMayac();
            }
        }
    }

    void isMoving()
    {
        isMove = true;
    }
}