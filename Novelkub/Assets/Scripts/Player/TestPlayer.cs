using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TestPlayer : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    public PlayerInput Input { get; private set; }
    public InteractionManager interactionManager;

    private GameObject _nearObject;
    private GameObject _pressKey;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        Input.PlayerActions.Interaction.started += OnInteractionStarted;
    }
    
    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
        if (_nearObject != null)
        {
            interactionManager.Interaction(_nearObject);
            Debug.Log("NPC 상호작용");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            _nearObject = other.gameObject;
            Debug.Log("NPC 충돌");
        }
        else if (other.tag == "Evidence")
        {
            _nearObject = other.gameObject;
            Debug.Log("Evidence 충돌");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC" || other.tag == "Evidence")
        {
            _nearObject = null;
            if (interactionManager.isAction)
            {
                interactionManager.ExitDialog(out interactionManager.dialogIndex);
            }
        }
    }
}
