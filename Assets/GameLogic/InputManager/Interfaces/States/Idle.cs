using UnityEngine;

public class Idle : IState
{
    InputsManager inputManager;

    public Idle(InputsManager input) { inputManager = input; }

    public void Enter()
    {
        inputManager.pointEvent.AddListener(IdleEvents.OnPoint);
        inputManager.clickEvent.AddListener(IdleEvents.OnInteract);
        inputManager.holdStartEvent.AddListener(IdleEvents.OnHold);
        inputManager.holdCancelEvent.AddListener(IdleEvents.OnRelease);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        inputManager.pointEvent.RemoveListener(IdleEvents.OnPoint);
        inputManager.clickEvent.RemoveListener(IdleEvents.OnInteract);
        inputManager.holdStartEvent.RemoveListener(IdleEvents.OnHold);
        inputManager.holdCancelEvent.RemoveListener(IdleEvents.OnRelease);
    }
    



}
