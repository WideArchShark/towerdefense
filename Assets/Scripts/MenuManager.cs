using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameSettings gameSettings;

    public void Awake()
    {
        gameSettings.startingWave = Random.Range(0, 15);
    }
    public void OnNewGameClick()
    {
        SceneManager.LoadScene("Enemy Wave Scene");
    }

    public void OnQuitClick()
    {

    }
}
