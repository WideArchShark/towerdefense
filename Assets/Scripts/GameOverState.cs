using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameState
{
    public override void EnterState(GameStateManager stateManager)
    {
        stateManager.gameOverPanel.SetActive(true);
    }

    public override void LostState(GameStateManager stateManager)
    {
        // throw new System.NotImplementedException();
    }

    public override void UpdateState(GameStateManager stateManager)
    {
        // throw new System.NotImplementedException();
    }
}
