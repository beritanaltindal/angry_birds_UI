using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Information : MonoBehaviour {
    public static void Save(List <World> worlds)
    {
        for (int i=0; i<GameControl.NumWorld; i++)
       {
            string world = "WORLD" + i.ToString();
            for (int j=0; j < GameControl.NumLevels; j++)
          {
                string level = "LEVEL" + j.ToString();
                PlayerPrefs.SetInt(world+level+"highscore", worlds[i].Levels[j].Highscore);

                PlayerPrefs.SetString(world+level+"unlocked", worlds[i].Levels[i].Unlocked?"true":"false");
            }

        }
        PlayerPrefs.Save();
    }

	public static List<World> Load()
    {
        List<World> tempWorld = new List<World>();

        for (int i=0; i<GameControl.NumWorld; i++)
        {
            tempWorld.Add(new World());
        }

        if (PlayerPrefs.HasKey("WORLD0LEVEL0highscore"))
        {
            for (int i = 0; i < GameControl.NumWorld; i++)
            {
                string world = "WORLD" + i.ToString();
                for (int j = 0; j < GameControl.NumLevels; j++)
                {
                    string level = "LEVEL" + j.ToString();

                    tempWorld[i].Levels[j].Highscore = PlayerPrefs.GetInt(world + level + "highscore");
                    tempWorld[i].Levels[j].Unlocked = PlayerPrefs.GetString(world + level + "unlocked") == "true" ? true : false;


                }
            }
        }
        else
        {
            Debug.Log("Doesn't contain key");
            Save(tempWorld);
        }

        return tempWorld;

    }
}
