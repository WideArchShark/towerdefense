using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGridState : GameState
{
    private PathGenerator pathGenerator;
    private int pathGenerationCount = 0;

    // I've done the whole grid generation in one go. If this does turn out to
    // be a bit slow, consider a coroutine, or maybe some gradual work in the
    // update function?
    public override void EnterState(GameStateManager stateManager)
    {
        pathGenerator = new PathGenerator(stateManager.gridWidth, stateManager.gridHeight);
        stateManager.pathGenerator = pathGenerator;
   }

    
    public override void UpdateState(GameStateManager stateManager) 
    {
        pathGenerationCount++;

        // Take multiple attempts at getting a big enough path each frame
        while (pathGenerationCount % 20 != 0)
        {
            pathGenerator.GeneratePath();
            while (pathGenerator.GenerateCrossroads()) ; // Create as many crossroad loops as you can
            pathGenerator.GenerateRoute();
            int pathSize = pathGenerator.pathRoute.Count;
            
            if (pathSize >= stateManager.minPathLength)
            {
                Debug.Log("Found a path of " + pathSize + " length, after " + pathGenerationCount + " attempt(s)");
                // I am nervous. Is it possible the change state below will work, but another call to
                // update might happen? That would result in a probably lower size path! Maybe I'm
                // too paranoid. Testing so far seems to be ok.
                stateManager.ChangeState(stateManager.displayGridState);
                break; // We found what we want... Break out of the while loop now. We're all done.
            }
            else
            {
                pathGenerationCount++;
            }
        }
    }

    public override void LostState(GameStateManager stateManager) { }
}
