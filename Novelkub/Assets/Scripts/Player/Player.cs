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
    public PlayerInput Input { get; private set; }

    public InteractionManager interactionManager;

    private GameObject _nearObject;
    private GameObject _pressKey;
    public CharacterController Controller { get; private set; }
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
            interactionManager.Interaction(_nearObject);
            Debug.Log("NPC 상호작용");
        }
    }

    private void OnCancelStarted(InputAction.CallbackContext context)
    {
        if (interactionManager.isAction)
        {
            interactionManager.ExitDialog(out interactionManager.dialogIndex);
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
