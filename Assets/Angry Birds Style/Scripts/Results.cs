using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    Text levelClearedText;
    Text scoreText;
    Text highscoreText;
    Button reset;
    Button back;
    Button next;
    GameObject stars;
    GameObject failed;

    void Start()
    {
        levelClearedText = transform.FindChild("Header").FindChild("LevelClearedText").GetComponent<Text>();
        scoreText = levelClearedText.transform.FindChild("InfoPanel").FindChild("ScoreText").GetComponent<Text>();
        highscoreText = scoreText.transform.FindChild("BestScoreText").GetComponent<Text>();
        stars = levelClearedText.transform.FindChild("InfoPanel").FindChild("Stars").gameObject;
        failed = levelClearedText.transform.FindChild("InfoPanel").FindChild("Failed").gameObject;
        reset = transform.FindChild("ButtonsPanel").FindChild("ResetButton").GetComponent<Button>();
        reset.onClick.AddListener( ()=> FindObjectOfType<UIManager>().Clicked(reset));
        back = transform.FindChild("ButtonsPanel").FindChild("BackButton").GetComponent<Button>();
        back.onClick.AddListener(()=>FindObjectOfType<UIManager>().Clicked(back));
        next = transform.FindChild("ButtonsPanel").FindChild("NextButton").GetComponent<Button>();
        next.onClick.AddListener(() => FindObjectOfType<UIManager>().Clicked(next));
    }


    public void UpdateResults(UIManager manager)
    {
        Level currLevel = manager.Control.CurrLevel;
        if (manager.Control.CurrentGame.numProjectiles > 0)
            currLevel.Currentscore += (manager.Control.CurrentGame.numProjectiles*10000);
        if (currLevel.CurrentDefeated)
        {
            stars.SetActive(true);
            failed.SetActive(false);
            next.interactable = true;
            levelClearedText.text = "LEVEL CLEARED!";

            if (manager.LevelIndex == GameControl.NumLevels - 1)
            {
                levelClearedText.text = "WORLD DEFEATED!";
                next.interactable = false;
            }
            else
            {
                manager.Control.CurrWorld.Levels[manager.LevelIndex + 1].Unlocked = true;

            }
            scoreText.text = "SCORE:" + currLevel.Currentscore;

            if (currLevel.Highscore < currLevel.Currentscore)
            {
                highscoreText.text = "new highscore!";
                currLevel.Highscore = currLevel.Currentscore;

            }
            else if (currLevel.Highscore >= currLevel.Currentscore)
            {
                highscoreText.text = "best" + currLevel.Highscore;
            }

            for (int i = 0; i < stars.transform.childCount; i++)
            {
                stars.transform.GetChild(i).gameObject.SetActive(false);
                if (currLevel.CurrStarScore > i)
                    StartCoroutine(ShowStar(i));
            }
        }
        else
        {
            levelClearedText.text = "Level Failed.";
            scoreText.text = "";
            highscoreText.text = "";
            stars.SetActive(false);
            failed.SetActive(true);
            if (!currLevel.Defeated)
                next.interactable = false;

        }
            currLevel.Currentscore = 0;

        

    }
    IEnumerator ShowStar(int index)
    {
        yield return new WaitForSeconds(index * 0.4f);
        stars.transform.GetChild(index).gameObject.SetActive(true);
    }
}
