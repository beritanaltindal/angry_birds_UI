using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {
   
    [SerializeField]
    Game gamePrefab;

    public Game CurrentGame;

    public const int NumWorld = 3;
    public const int NumLevels = 20;
    public List<World> AllWorlds;

    private Level currLevel;
    private World currWorld;
      

    public World CurrWorld
    {
        set
        {
            currWorld = value;
            Debug.Log("Current World set.");
        }
        get
        {
            if (currWorld != null)
                return currWorld;
            else
            {
                Debug.Log("Can't retrieve current world. Valuse is null");
                return null;
            }
        }
    }

    public Level CurrLevel
    {
        set
        {
            currLevel = value;
            Debug.Log("Current Level set.");
        }
        get
        {
            if (currLevel != null)
                return currLevel;
            else
            {
                Debug.Log("Can't retrieve current level. Valuse is null");
                return null;
            }
        }
    }
    void Start ()
    {
        AllWorlds = Information.Load();

	}
    void OnApplicationQuit()
    {
        Information.Save(AllWorlds); ;
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        AllWorlds = Information.Load();

    }
	
	
	void Update ()
    {
        if (CurrentGame != null)
        {
            CheckForCurrentGameOver();
        }
	}

    void CheckForCurrentGameOver()
    {
        if (CurrentGame.currLevelObj == null && CurrentGame.gameOver)
        {
            
            Debug.Log("Game Over!");
        }
    }
    public void StartGame(int level, int world)
    {
        CurrentGame = Instantiate(gamePrefab) as Game;
        CurrentGame.gameObject.name = "Game";

        if (world == 1)
            level += 20;
        else if (world == 2)
            level += 40;

        CurrentGame.InitLevel(level, 3);
    }
}
