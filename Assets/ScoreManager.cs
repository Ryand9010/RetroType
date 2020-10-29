using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

 

    public TextMeshProUGUI scoreText;


    public float scoreCount;



    //amount gained or lost when typing a letter
    public float letterPoint = 10f;


    public void GivePoints(int multiplier)
    {
        scoreCount += letterPoint * multiplier;      
    }

    

    public void LosePoints()
    {
        //scoreCount -= 10f;
        scoreText.color = Color.red;
    }

   

    private void Update()
    {

        scoreText.text = "Score: " + scoreCount;
        if (scoreCount < 0)
        {
            scoreText.color = Color.red;
        }
    }

}
