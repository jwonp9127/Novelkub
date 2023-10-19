using System.Collections;
using System.Collections.Generic;
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
            InteractionManager.ExitDialog(out InteractionManager.dialogIndex);
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
        else if (other.tag == "Evidence")
        {
            _nearObject = other.gameObject;
            Debug.Log("Evidence 충돌");
        }
        else if (other.tag == "TimeLine")
        {
            _nearObject = other.gameObject;
            Debug.Log("timelinearea 충돌");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC" || other.tag == "Evidence")
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
