using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public DialogueSystem dialogueSystem; // ��ȭ �ý��� ��ũ��Ʈ �Ǵ� ������Ʈ

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
            // E Ű�� ������ �� ��ȭ �ý����� �����ϵ��� ȣ��
            dialogueSystem.StartDialogue();
        }
    }
}
