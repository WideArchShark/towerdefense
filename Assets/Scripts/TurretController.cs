using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // public GameObject gameManager; // We'll pass this in properly later.
    private GameStateManager stateManager;

    public float rateOfFire;
    public int health;
    public float targetRange;
    public float acquisitionRate;
    private GameObject activeEnemy;

    private TurretState currentState;

    enum TurretState { Patrolling, TargetFound, TargetAcquired, TargetLost }

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameStateManager.instance;
        currentState = TurretState.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TurretState.Patrolling:
                PatrollingUpdate();
                break;
            case TurretState.TargetFound:
                TargetFoundUpdate();
                break;
            case TurretState.TargetAcquired:
                TargetAcquiredUpdate();
                break;
            case TurretState.TargetLost:
                TargetLostUpdate();
                break;
            default:
                Debug.LogError("Invalid state: " + currentState);
                break;
        }
    }

    private void TargetLostUpdate()
    {
        currentState = TurretState.Patrolling;
    }

    private void TargetAcquiredUpdate()
    {
        if (activeEnemy != null && Vector3.Distance(transform.position, activeEnemy.transform.position) < targetRange)
        {
            Vector3 enemyPosition = new Vector3(activeEnemy.transform.position.x, transform.position.y, activeEnemy.transform.position.z);
            transform.LookAt(enemyPosition);
        }
        else
        {
            currentState = TurretState.TargetLost;
        }
    }

    private void TargetFoundUpdate()
    {
        if (activeEnemy != null)
        {
            Vector3 directionRequired = new Vector3(activeEnemy.transform.position.x, transform.position.y, activeEnemy.transform.position.z) - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionRequired, Time.deltaTime * acquisitionRate, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            Quaternion targetRotation = Quaternion.LookRotation(directionRequired);

            if (Quaternion.Angle(transform.rotation, targetRotation) <= 0.01f)
            {
                // Debug.Log("Target Acquired");
                currentState = TurretState.TargetAcquired;
            }
        }
        else
        {
            currentState = TurretState.TargetLost;
        }
    }

    private void PatrollingUpdate()
    {
        float nearestEnemy = targetRange;
        bool enemyFound = false;

        for (int i = 0; i < stateManager.activeEnemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, stateManager.activeEnemies[i].transform.position) < nearestEnemy)
            {
                // Debug.Log("Target Found " + stateManager.activeEnemies[i]);
                nearestEnemy = Vector3.Distance(transform.position, stateManager.activeEnemies[i].transform.position);
                activeEnemy = stateManager.activeEnemies[i];
                enemyFound = true;
            }
        }

        if (enemyFound)
        {
            currentState = TurretState.TargetFound;
        }
    }
}

