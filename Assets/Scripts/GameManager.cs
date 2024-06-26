using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDifficulty = Difficulty.difficulty;

public class GameManager : MonoBehaviour
{
    private bool isBallCatched;
    public GameDifficulty gameDifficulty;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return; }            
        Instance = this;
        DontDestroyOnLoad(gameObject);

        isBallCatched = false;
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

    //Checking for Ball catching state
    public void BallWasCatched() { isBallCatched = true; }
    public bool IsBallCatched() { return isBallCatched; }

    
}
