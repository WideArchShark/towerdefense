using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public int damage;

    private int nextPathIndex;
    private Vector3 nextPathVector3;

    [HideInInspector] public GameStateManager stateManager;
    private List<Vector2Int> pathRoute;

    // Start is called before the first frame update
    void Start()
    {
        pathRoute = stateManager.pathGenerator.pathRoute;
        transform.position = new Vector3(pathRoute[0].x, 0.2f, pathRoute[0].y);
        nextPathIndex = 1;
        nextPathVector3 = new Vector3(pathRoute[nextPathIndex].x, 0.2f, pathRoute[nextPathIndex].y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPathVector3, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPathVector3) < 0.05f)
        {
            nextPathIndex++;

            if (nextPathIndex == pathRoute.Count)
            {
                stateManager.playerRemainingHealth -= damage;
                stateManager.activeEnemies.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                nextPathVector3 = new Vector3(pathRoute[nextPathIndex].x, 0.2f, pathRoute[nextPathIndex].y);
            }
            
        }
    }
}
