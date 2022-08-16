using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState currentState;

    // Define all of the variables/classes that the various states may need to reference.
    // These need to be set in the inspector.
    public int gridWidth, gridHeight, minPathLength;
    public GridCellObject[] pathCellObjects;
    public GridCellObject[] sceneryCellObjects;
    public GameObject gameOverPanel;
    public GameObject healthBar;
    public GameSettings gameSettings;

    public GameObject[] enemyWave;
    public List<GameObject> activeEnemies;

    public int playerMaxHealth;
    [HideInInspector] public int playerRemainingHealth;
    [HideInInspector] public TextMeshProUGUI healthText;
    [HideInInspector] public PlayerInput playerInput;


    // Define the variables that will get set by the actual states. Most of these won't
    // appear in the inspector as they're not MonoBehaviour classes, or generic types.
    // But if they are appearing, add [HideInInspector] to the front. All variables 
    // below should not be being touched in the Inspector.
    public PathGenerator pathGenerator;

    public GameState generateGridState = new GenerateGridState();
    public GameState displayGridState = new DisplayGridState();
    public GameState enemyWaveState = new EnemyWaveState();
    public GameState userWaveSetupState = new UserWaveSetupState();
    public GameState gameOverState = new GameOverState();

    Slider healthBarSlider;

    public static GameStateManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogError("Second instance of GameStateManager is being created");
            Destroy(this.gameObject);
        }
    }



    // Start is called before the first frame update
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentState = generateGridState;
        currentState.EnterState(this);

        playerRemainingHealth = playerMaxHealth;
        healthBarSlider = healthBar.GetComponent<Slider>();

        Debug.Log("Game Settings Starting Wave = " + gameSettings.startingWave);
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.UpdateState(this);
        healthText.text = "Health: " + Mathf.Max(0, playerRemainingHealth);
        healthBarSlider.value = (float)playerRemainingHealth / (float)playerMaxHealth;
    }

    public void ChangeState(GameState newState)
    {
        currentState.LostState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

}
