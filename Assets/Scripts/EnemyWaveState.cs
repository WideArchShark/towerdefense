using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveState : GameState
{
    private int enemySpawnIndex = 0;
    private float waitTimeInSeconds;
    private float lastSpawnTime;

    public override void EnterState(GameStateManager stateManager)
    {
        waitTimeInSeconds = 2f;
        lastSpawnTime = Time.time;
        stateManager.activeEnemies = new List<GameObject>();
    }


    public override void UpdateState (GameStateManager stateManager)
    {
        if (Time.time - lastSpawnTime > waitTimeInSeconds && enemySpawnIndex < stateManager.enemyWave.Length)
        {
            Vector3 startingPosition = new Vector3(stateManager.pathGenerator.pathRoute[0].x, 0.2f, stateManager.pathGenerator.pathRoute[0].y);
            GameObject enemy = GameObject.Instantiate(stateManager.enemyWave[enemySpawnIndex], startingPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().stateManager = stateManager;
            stateManager.activeEnemies.Add(enemy);

            lastSpawnTime = Time.time;
            enemySpawnIndex++;
        }

        if (stateManager.playerRemainingHealth <= 0)
        {
            stateManager.ChangeState(stateManager.gameOverState);
        }
    }

    public override void LostState(GameStateManager stateManager)
    {

    }
}
