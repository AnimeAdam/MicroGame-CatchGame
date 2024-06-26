using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

using GameDifficulty = Difficulty.difficulty;

public class SceneManager : MonoBehaviour
{
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void GoToGameDifficultyEasy()
    {
        GameManager.Instance.SetDifficulty(GameDifficulty.Easy);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoToGameDifficultyMedium()
    {
        GameManager.Instance.SetDifficulty(GameDifficulty.Medium);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoToGameDifficultyHard()
    {
        GameManager.Instance.SetDifficulty(GameDifficulty.Hard);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
