using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {

        Scene scene = SceneManager.GetActiveScene();


        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);


        switch(scene.name)
        {
            case "EasyLeaderboard":
                CheckFile("easy");
                break;
            case "MediumLeaderboard":
                CheckFile("medium");
                break;
            case "HardLeaderboard":
                CheckFile("hard");
                break;
        }
    }

    private void CheckFile(string newFilename)
    {

        if (File.Exists(Application.dataPath + "/" + newFilename + "Leaderboard.json"))
        {

            string json = File.ReadAllText(Application.dataPath + "/" + newFilename + "Leaderboard.json");
            Highscores highscores = JsonUtility.FromJson<Highscores>(json);
            LoadSortDisplayLeaderboard(highscores);
        }
        else
        {

            highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
                new HighscoreEntry{score = 0, name = "---"},
            };

            Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
            string json = JsonUtility.ToJson(highscores);
            File.WriteAllText(Application.dataPath + "/" + newFilename + "Leaderboard.json", json);
            LoadSortDisplayLeaderboard(highscores);
        }
    }

    public void LoadSortDisplayLeaderboard (Highscores hscore)
    {


        //sorting
        for (int i = 0; i < hscore.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < hscore.highscoreEntryList.Count; j++)
            {
                if (hscore.highscoreEntryList[j].score > hscore.highscoreEntryList[i].score)
                {
                    //Swap
                    HighscoreEntry temp = hscore.highscoreEntryList[i];
                    hscore.highscoreEntryList[i] = hscore.highscoreEntryList[j];
                    hscore.highscoreEntryList[j] = temp;
                }
            }
        }
        
        

        highscoreEntryTransformList = new List<Transform>();
        for (int i = 0; i < 10; i++)
        {
            CreateHighscoreEntryTransform(hscore.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
        }
    }


    //Creating an "Entry"
    public void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 10f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, - templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "th";
                break;

            case 1:
                rankString = "1st";
                break;
            case 2:
                rankString = "2nd";
                break;
            case 3:
                rankString = "3rd";
                break;

        }

        //set position
        //first child - Rank
        var pText = entryTransform.GetChild(0);
        pText.GetComponent<TextMeshProUGUI>().text = rankString;

        //set score
        //second child - Score
        string score = highscoreEntry.score.ToString();
        var sText = entryTransform.GetChild(1);
        sText.GetComponent<TextMeshProUGUI>().text = score;


        //set name
        //third child = Name
        string name = highscoreEntry.name;
        var nText = entryTransform.GetChild(2);
        nText.GetComponent<TextMeshProUGUI>().text = name;


        //add to list of transforms to display
        transformList.Add(entryTransform);
    }

  
    //Check to see if player score is on the leaderboard
    public bool CheckScoreboard(int score)
    {
        
        string json = File.ReadAllText(Application.dataPath + "/" + PlayerPrefs.GetString("difficulty") + "leaderboard.json");
        Highscores highscores = JsonUtility.FromJson<Highscores>(json);


        if (score > highscores.highscoreEntryList[9].score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //see how close score is to 10th place
    public int CheckHowCloseScoreIs(int score)
    {
        string json = File.ReadAllText(Application.dataPath + "/" + PlayerPrefs.GetString("difficulty") + "leaderboard.json");
        Highscores highscores = JsonUtility.FromJson<Highscores>(json);

        int closeScore = highscores.highscoreEntryList[9].score - score;

        return closeScore;
        
    }



    public void AddHighScoreEntry(int score, string name)
    {
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load saved Highscores
        string json = File.ReadAllText(Application.dataPath + "/" + PlayerPrefs.GetString("difficulty") + "leaderboard.json");
        Highscores highscores = JsonUtility.FromJson<Highscores>(json);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);


        //sorting
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Swap
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }

      
        //Save updated Highscores
        string newJson = JsonUtility.ToJson(highscores);
        File.WriteAllText(Application.dataPath + "/" + PlayerPrefs.GetString("difficulty") + "leaderboard.json", newJson);

    }


    //holds lists of entries
    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    /*
     Score Entry
     */
    [System.Serializable]
     public class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
