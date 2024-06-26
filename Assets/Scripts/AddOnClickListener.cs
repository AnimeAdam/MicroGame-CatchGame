using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOnClickListener : MonoBehaviour
{
    private Button button;
    private GameObject gameManager;

    public enum MenuButton {None, Easy, Medium, Hard, Quit};
    public MenuButton menuButton;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager");
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetButton()
    {
        switch (menuButton)
        {
            case MenuButton.Easy:
                button.onClick.AddListener(gameManager.GetComponent<SceneManager>().GoToGameDifficultyEasy);
                break;
            case MenuButton.Medium:
                button.onClick.AddListener(gameManager.GetComponent<SceneManager>().GoToGameDifficultyMedium);
                break;
            case MenuButton.Hard:
                button.onClick.AddListener(gameManager.GetComponent<SceneManager>().GoToGameDifficultyHard);
                break;
            case MenuButton.Quit:
                button.onClick.AddListener(gameManager.GetComponent<SceneManager>().QuitGame);
                break;
            default:
                Debug.Log("The AddOnClickListener hasn't been added to " + gameObject.name);
                Debug.Break();
                break;
        }
    }
}