using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    //Text object for score on normal gameover screen
    public GameObject badGameScore;

    //Text object for dynamic message on normal gameover screen
    public GameObject dynamicMessage;

    //Text object to show score on high score screen
    public GameObject gameScore;

    //reference username input field
    public GameObject userName;

    //reference continue to leaderboard button
    public Button continueButton;

    //game over screen
    public GameObject gameOverUI;

    //game over screen if high score
    public GameObject highscoreUI;

    //reference to word manager
    public GameObject wordManager;

    //object that holds the menus
    public GameObject Menu;

    //Object to reference ScoreManager
    public GameObject scoreManager;

    //bools to manage gameover sounds
    private bool regularGameOverSound;
    private bool leadboardGameOverSound;

    private void Start()
    {
        wordManager = GameObject.FindGameObjectWithTag("Main");
        Menu = GameObject.FindGameObjectWithTag("UI");
        scoreManager = GameObject.Find("ScoreManager");




        //add onclick listener to continue button
        continueButton.onClick.AddListener(ContinueTask);

        //setting sounds to false
        regularGameOverSound = false;
        leadboardGameOverSound = false;
    }


    private void Update()
    {
         

        if (wordManager.GetComponent<WordManager>().isGameOver())
        {
            gameOverUI.SetActive(true);
            //checking to see if player's score was high enough to be placed in leaderboard(top 10)
            if (wordManager.GetComponent<HighscoreTable>().CheckScoreboard((int)scoreManager.GetComponent<ScoreManager>().scoreCount))
            {
                //disable pause ability
                Menu.GetComponent<PauseMenu>().enabled = false;
                //enable highscore screen
                highscoreUI.SetActive(true);
                //dynamically set score to whatever player's score was
                gameScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreManager.GetComponent<ScoreManager>().scoreCount.ToString();

                //play sound for highscore
                if(!leadboardGameOverSound)
                {
                    SoundManager.PlaySound("power_up_19");
                    leadboardGameOverSound = true;
                }

                //making sure something is entered in name field before allowing continue button to function
                if (!(userName.GetComponent<TMP_InputField>().text == "" ))
                {
                    continueButton.interactable = true;
                }


            }
            else //normal gameover screen
            {
                if(!regularGameOverSound)
                {
                    SoundManager.PlaySound("game_over_30");
                    regularGameOverSound = true;
                }

                if (wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) == 0)
                {

                    gameOverUI.SetActive(true);
                    badGameScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreManager.GetComponent<ScoreManager>().scoreCount.ToString();
                    dynamicMessage.GetComponent<TextMeshProUGUI>().fontSize = 23;
                    dynamicMessage.GetComponent<TextMeshProUGUI>().text = "So Close! You're tied for 10th!";
                }

                if (wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) >= 1 && wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) <= 300)
                {
                    
                    gameOverUI.SetActive(true);
                    badGameScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreManager.GetComponent<ScoreManager>().scoreCount.ToString();
                    dynamicMessage.GetComponent<TextMeshProUGUI>().fontSize = 23;
                    dynamicMessage.GetComponent<TextMeshProUGUI>().text = "Almost! You're " + System.Math.Abs(scoreManager.GetComponent<ScoreManager>().scoreCount) + " points short of the leaderboard!";
                }
                if (wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) > 300 && wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) <= 500)
                {
                    gameOverUI.SetActive(true);
                    badGameScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreManager.GetComponent<ScoreManager>().scoreCount.ToString();
                    dynamicMessage.GetComponent<TextMeshProUGUI>().fontSize = 23;
                    dynamicMessage.GetComponent<TextMeshProUGUI>().text = "Keep Practicing! You're " + System.Math.Abs(scoreManager.GetComponent<ScoreManager>().scoreCount) + " points short of the leaderboard!";
                }
                if (wordManager.GetComponent<HighscoreTable>().CheckHowCloseScoreIs((int)scoreManager.GetComponent<ScoreManager>().scoreCount) > 500)
                {
                    gameOverUI.SetActive(true);
                    badGameScore.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreManager.GetComponent<ScoreManager>().scoreCount.ToString();
                    dynamicMessage.GetComponent<TextMeshProUGUI>().fontSize = 23;
                    dynamicMessage.GetComponent<TextMeshProUGUI>().text = "Yikes! You have at least  " + System.Math.Abs(scoreManager.GetComponent<ScoreManager>().scoreCount) + " points to go!";
                }

                //disable pause ability
                Menu.GetComponent<PauseMenu>().enabled = false;
                
            }
            
        }
    }

    //when button is clicked, 
    public void ContinueTask()
    {
        wordManager.GetComponent<HighscoreTable>().AddHighScoreEntry((int)scoreManager.GetComponent<ScoreManager>().scoreCount, userName.GetComponent<TMP_InputField>().text);

        

        if(PlayerPrefs.GetString("difficulty") == "easy")
        {
            SceneManager.LoadScene("EasyLeaderboard");
        }
        if (PlayerPrefs.GetString("difficulty") == "medium")
        {
            SceneManager.LoadScene("MediumLeaderboard");
        }
        if (PlayerPrefs.GetString("difficulty") == "hard")
        {
            SceneManager.LoadScene("HardLeaderboard");
        }
    }


    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void ChangeDifficulty()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");   
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
