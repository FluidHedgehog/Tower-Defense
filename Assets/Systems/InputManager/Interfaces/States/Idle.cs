using UnityEngine;

public class Idle : IState
{
    InputManager inputManager;

    private Idle(InputManager input) { inputManager = input; }

    public void Enter()
    {
        //inputManager.clickEvent.AddListener(IdleEvents.DetectHold);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        //inputManager.clickEvent.RemoveListener(IdleEvents.DetectHold);
    }
    



}
