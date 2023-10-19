using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    private PlayerInput Input { get; set; }
    public InteractionManager interactionManager;
    public TimelineManager timelineManager;
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
        Input.PlayerActions.Cancel.started += OnCancelStarted;
    }
    
    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
        if (_nearObject != null)
        {
            if (_nearObject.name == "Take1StartArea")
            {
               // timelineManager.Take1();
            }
            else if (_nearObject.name == "Take2StartArea")
            {
               // timelineManager.Take2();
            }
            else
            {
                interactionManager.Interaction(_nearObject);
                Debug.Log("NPC 상호작용");               
            }
        }
    }

    private void OnCancelStarted(InputAction.CallbackContext context)
    {
        if (interactionManager.isAction)
        {
            interactionManager.ExitDialog(out interactionManager.dialogIndex);
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
}
