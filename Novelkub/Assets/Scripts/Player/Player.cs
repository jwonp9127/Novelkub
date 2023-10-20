using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public Collider Collider { get; private set; }

    public PlayerInput Input { get; private set; }

    public GameObject manager;
    public InteractionManager InteractionManager { get; private set; }
    public TimelineManager TimelineManager { get; private set; }

    private GameObject _nearObject;
    private GameObject _pressKey;
    public CharacterController Controller { get; private set; }
    public Collider InteractionArea { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    private int DestroyTrashCount = 0;


	private void Awake()
    {
        AnimationData.Initialize();
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponentInChildren<ForceReceiver>();
        InteractionManager = manager.GetComponent<InteractionManager>();
        TimelineManager = manager.GetComponent<TimelineManager>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        Input.PlayerActions.Interaction.started += OnInteractionStarted;
        Input.PlayerActions.Cancel.started += OnCancelStarted;
        Input.PlayerActions.OneInvne.started += OneInvne_started;
        Input.PlayerActions.TwoInven.started += TwoInven_started;
        Input.PlayerActions.ThreeInven.started += ThreeInven_started;
        Input.PlayerActions.FourInven.started += FourInven_started;
        Input.PlayerActions.FiveInven.started += FiveInven_started;
        Input.PlayerActions.SixInven.started += SixInven_started;
        Input.PlayerActions.SevenInven.started += SevenInven_started;
        Input.PlayerActions.EightInven.started += EightInven_started;
    }

    private void EightInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(7);
    }

    private void SevenInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(6);
    }

    private void SixInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(5);
    }

    private void FiveInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(4);
    }

    private void FourInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(3);
    }

    private void ThreeInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(2);
    }

    private void TwoInven_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(1);
    }

    private void OneInvne_started(InputAction.CallbackContext obj)
    {
        Inventory.instance.SelectItem(0);
        Debug.Log("가능가능");
    }

    public void Teleport(Vector3 spawnPosition)
    {
        Controller.enabled = false;
        transform.position = spawnPosition;
        Controller.enabled = true;
    }

    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
        if (_nearObject != null)
        {
            if (_nearObject.name == "Take1StartArea")
            {
                TimelineManager.Take1();
            }
            else if (_nearObject.name == "Take2StartArea")
            {
                TimelineManager.Take2();
            }
			else if (_nearObject.CompareTag("Trash"))
			{
                Destroy(_nearObject);
                DestroyTrashCount++;
                if(DestroyTrashCount == 7)
                {
					InteractionManager.FinishMiniGame();
				}
				Debug.Log("쓰레기를 치워따");
			}
            else
            {
                InteractionManager.Interaction(_nearObject);
                Debug.Log("NPC 상호작용");               
            }
        }
    }

    private void OnCancelStarted(InputAction.CallbackContext context)
    {
        if (InteractionManager.isAction)
        {
            InteractionManager.instance.dialogUI.SetActive(!InteractionManager.instance.isDialogObject);
            InteractionManager.instance.isDialogObject = !InteractionManager.instance.isDialogObject;
            //InteractionManager.ExitDialog(out InteractionManager.dialogIndex);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            _nearObject = other.gameObject;
            Debug.Log("NPC 충돌");
            //_nearObject
        }
        else if (other.tag == "Trash")
        {
            _nearObject = other.gameObject;
            Debug.Log("Trash 충돌");
        }
        else if (other.tag == "TimeLine")
        {
            _nearObject = other.gameObject;
            Debug.Log("timelineArea 충돌");
        }
        else if (other.tag == "InteractableObject")
        {
            _nearObject = other.gameObject;
            Debug.Log("InteractableObject 충돌");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC" || other.tag == "Trash" || other.tag == "InteractableObject")
        {
            _nearObject = null;
        }
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

}
