using UnityEngine;

public class SpellState : IState
{
    InputsManager inputManager;

    public SpellState(InputsManager input) { inputManager = input; }

    public void Enter()
    {
        inputManager.pointEvent.AddListener(SpellStateEvents.OnPoint);
        inputManager.clickEvent.AddListener(SpellStateEvents.OnInteract);
        inputManager.holdStartEvent.AddListener(SpellStateEvents.OnHold);
        inputManager.holdCancelEvent.AddListener(SpellStateEvents.OnRelease);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        inputManager.pointEvent.RemoveListener(SpellStateEvents.OnPoint);
        inputManager.clickEvent.RemoveListener(SpellStateEvents.OnInteract);
        inputManager.holdStartEvent.RemoveListener(SpellStateEvents.OnHold);
        inputManager.holdCancelEvent.RemoveListener(SpellStateEvents.OnRelease);
    }
}
