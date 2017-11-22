using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    Text levelText;
    Text scoreText;
    Text highscoreText;
    GameObject projectilesDisplay;


    public GameObject projectilePrefab;
    UIManager mgr;

    void Start()
    {
        mgr = FindObjectOfType<UIManager>();
        scoreText = transform.FindChild("Score").GetComponent<Text>();
        highscoreText = scoreText.transform.FindChild("Highscore").GetComponent<Text>();
        levelText = transform.FindChild("Level").GetComponent<Text>();
        projectilesDisplay = transform.FindChild("Projectiles").gameObject;
    }
    public void UpdateHUD(int score, int highscore, int level, int numprojectiles)
    {
        scoreText.text = "SCORE\n" + score.ToString();
        if (highscore > 0)
            highscoreText.text = "highscore\n" + highscore.ToString();
        else
            highscoreText.text = "";
        levelText.text = "LEVEL:" + level.ToString();


        UpdateProjectiles(numprojectiles);   
    }

    void UpdateProjectiles(int count)
    {
        int currDisplayed = projectilesDisplay.transform.childCount;
        if(count != currDisplayed)
        {
            if(count < currDisplayed)
            {
                for(int i=currDisplayed; i>count; i--)
                {
                    Destroy(projectilesDisplay.transform.GetChild(i-1).gameObject);
                }
            } else if (count > currDisplayed)
            {
                for (int i=currDisplayed; i < count; i++)
                {
                    GameObject temp = mgr.InitUIElement(projectilePrefab, projectilesDisplay.transform);
                    RectTransform rect = temp.GetComponent<RectTransform>();
                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50*i, 50);
                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 50);

                }
            }
        }
    }



}
