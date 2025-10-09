using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private IState currentState;
    [SerializeField] private InputsManager inputManager;

    public IState idleState;
    public IState placeTowerState;

    void Start()
    {
        idleState = new Idle(inputManager);
        placeTowerState = new PlaceTower(inputManager);
    }

    void OnEnable()
    {
        inputManager.pointEvent.AddListener(OnPoint);
        inputManager.clickEvent.AddListener(OnInteract);
        inputManager.holdStartEvent.AddListener(OnHold);
        inputManager.holdCancelEvent.AddListener(OnRelease);
    }

    void OnDisable()
    {
        inputManager.pointEvent.RemoveListener(OnPoint);
        inputManager.clickEvent.RemoveListener(OnInteract);
        inputManager.holdStartEvent.RemoveListener(OnHold);
        inputManager.holdCancelEvent.RemoveListener(OnRelease);        
    }

    void Update()
    {
        if (currentState != null) currentState.Update();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    void OnPoint(Vector2 mousePos)
    {
        
    }

    void OnInteract()
    {

    }

    void OnHold()
    {

    }

    void OnRelease()
    {
        
    }
}
