using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using GameDifficulty = Difficulty.difficulty;

public class GameManager : MonoBehaviour
{
    private bool freezeGlove;
    public GameDifficulty gameDifficulty;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return; }            
        Instance = this;
        DontDestroyOnLoad(gameObject);

        freezeGlove = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetDifficulty(GameDifficulty difficulty)
    {
        gameDifficulty = difficulty;
    }

    //Checking for when to freeze the position of the glove.
    public void FreezeTheGlove() { freezeGlove = true; }
    public void UnFreezeTheGlove() { freezeGlove = false; }
    public bool IsTheGloveFreezen() { return freezeGlove; }

    #region ShowWinLoseText

    public void ShowGameOver()
    {
        FindInActiveObjectByName("YouMissedText").GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
        FindInActiveObjectByName("BackToMenuButton").GetComponent<Button>().gameObject.SetActive(true);
    }

    public void ShowWinning()
    {
        FindInActiveObjectByName("WellDoneText").GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
        FindInActiveObjectByName("BackToMenuButton").GetComponent<Button>().gameObject.SetActive(true);
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    #endregion ShowWinLoseText

    
}
