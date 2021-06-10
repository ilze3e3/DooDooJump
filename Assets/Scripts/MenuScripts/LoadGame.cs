using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    // Load Game Scene

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("GameScene");

    }
    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("MenuScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
