using System;
using UnityEngine;

public class DisplayGridState : GameState
{
    // Again, I'm doing the whole thing in the startup. If this is clunky, then
    // think of a way to stick a gradual cell object instantiation in the update
    // script or something.
    public override void EnterState(GameStateManager stateManager)
    {
        // Put the path down.
        foreach (Vector2Int pathCell in stateManager.pathGenerator.pathCells)
        {
            int neighbourValue = stateManager.pathGenerator.getCellNeighbourValue(pathCell.x, pathCell.y);
            GameObject pathTile = stateManager.pathCellObjects[neighbourValue].cellPrefab;

            GameObject pathTileCell = MonoBehaviour.Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, stateManager.pathCellObjects[neighbourValue].yRotation, 0f, Space.Self);
        }

        // Now lay the "scenery" tiles...
        for (int y = stateManager.gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < stateManager.gridWidth; x++)
            {
                if (stateManager.pathGenerator.CellIsEmpty(x, y))
                {
                    int randomSceneryCellIndex = UnityEngine.Random.Range(0, stateManager.sceneryCellObjects.Length);
                    MonoBehaviour.Instantiate(stateManager.sceneryCellObjects[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    
                    //GameObject lumberjack = MonoBehaviour.Instantiate(stateManager.lumberjackGameObject, new Vector3(x, 0.25f, y), Quaternion.identity);
                    //lumberjack.GetComponent<Animator>().SetBool("isFelling", true);
                }
            }
        }

        stateManager.ChangeState(stateManager.enemyWaveState);
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