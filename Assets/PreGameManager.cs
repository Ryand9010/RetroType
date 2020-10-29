using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PreGameManager : MonoBehaviour
{
    //text for tooltip
    public TextMeshProUGUI toolTip;

    //also getting the object of the tooltip to set it active/unactive
    public GameObject difficultyTip;

    public bool clicked = false;

    public void Start()
    {
        difficultyTip.SetActive(true);
        difficultyTip = GameObject.FindGameObjectWithTag("ToolTip");      
    }

    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("difficulty", difficulty);
    }

    public void DisplayToolTip()
    {
        if(clicked)
        {
            if (PlayerPrefs.GetString("difficulty") == "easy")
            {
                toolTip.SetText("Words up to 4 letters long. Recommended for beginners.");
            }
            if (PlayerPrefs.GetString("difficulty") == "medium")
            {
                toolTip.SetText("Words up to 8 letters long. Best for intermediate skill levels");
            }
            if (PlayerPrefs.GetString("difficulty") == "hard")
            {
                toolTip.SetText("Words up to 13 letters long! Only for extreme typists!");
            }

            difficultyTip.SetActive(true);
        }
        else
        {
            difficultyTip.SetActive(false);
        }
    }

    public void IsClicked()
    {
        clicked = true;
       
    }

    public void StartGame()
    {
        if (clicked)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //setting tool tip to a blank string for when its returned to
            toolTip.SetText("");
        }
        
    }

    //Go back to main menu
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    

}
