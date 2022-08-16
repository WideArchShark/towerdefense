using UnityEngine;

public abstract class GameState
{
    // This will be called when the new state begins.
    public abstract void EnterState(GameStateManager stateManager);


    // This will be called each frame.
    public abstract void UpdateState(GameStateManager stateManager);


    // Will be called when the GameStateManager.ChangeState method is run. So any cleanup here :)
    public abstract void LostState(GameStateManager stateManager);

    public override string ToString()
    {
        return "TEST";
    }
}
