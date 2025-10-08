using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputsManager : MonoBehaviour
{
    public static InputsManager Instance { get; private set; }

    public class Vector2Event : UnityEvent<Vector2> { }
        public Vector2Event pointEvent = new Vector2Event();

    public class ButtonEvent : UnityEvent { }
        public ButtonEvent clickEvent = new ButtonEvent();
        public ButtonEvent holdStartEvent = new ButtonEvent();
        public ButtonEvent holdCancelEvent = new ButtonEvent();

    InputSystem_Actions input;
    Vector2 mousePos;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        input = new InputSystem_Actions();
    }

    void Start()
    {
        input.Player.Point.performed += ctx => OnPoint();

        input.Player.Interact.started += OnInteract;
        input.Player.Interact.performed += OnInteract;
        input.Player.Interact.canceled += OnInteract;

        // input.Player.Hold.started += OnHold;
        // input.Player.Hold.canceled += OnHold;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(input.Player.Point.ReadValue<Vector2>());
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void OnPoint()
    {
        pointEvent.Invoke(mousePos);
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is TapInteraction)
        {
            if (ctx.performed)
            {
                Debug.Log("Interact!");
                clickEvent.Invoke();
            }

        }
        else if (ctx.interaction is HoldInteraction)
        {
            if (ctx.started)
            {
                Debug.Log("Hold!");
                holdStartEvent.Invoke();
            }
            if (ctx.canceled)
            {
                Debug.Log("Cancel!");
                holdCancelEvent.Invoke();
            }
        }
    }
}
