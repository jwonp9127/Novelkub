using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public DialogueSystem dialogueSystem; // 대화 시스템 스크립트 또는 컴포넌트

    private bool isInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // E 키를 눌렀을 때 대화 시스템을 시작하도록 호출
            dialogueSystem.StartDialogue();
        }
    }
}
