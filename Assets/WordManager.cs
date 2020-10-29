using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public bool gameEnd = false;

    public bool hasActiveWord;

    public Word activeWord;

    public WordSpawn wordSpawn;
    
    public ScoreManager scoreManager;

    public TextMeshProUGUI bonusText;

    public int multiplier = 1;

    public int streakCounter = 0;

    public float penalties = 0;

    public GameObject scorePrefab;

    public Transform wordCanvas;

    private void Start()
    {
        WordDisplay.fallSpeed = 1f;
    }

    //adding words to List of words
    public void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawn.SpawnWord());
       
        words.Add(word);
        
    }



    // setting multiplier based on streakcounter
    public void GetMultiplier()
    {
        if (streakCounter >= 0 && streakCounter < 12)
        {
            
            bonusText.SetText("");
            bonusText.color = Color.green;
            scoreManager.scoreText.color = Color.green;
            activeWord.display.text.color = Color.green;
        }
        if (streakCounter >= 12 && streakCounter < 24)
        {

            
            multiplier = 2;
            bonusText.SetText("X2 Bonus!");
            bonusText.color = Color.cyan;
            scoreManager.scoreText.color = Color.cyan;
            activeWord.display.text.color = Color.cyan;
            

        }
        if (streakCounter >= 24 && streakCounter < 48)
        {
            
            multiplier = 3;
            bonusText.SetText("X3 Bonus!");
            bonusText.color = Color.yellow;
            scoreManager.scoreText.color = Color.yellow;
            activeWord.display.text.color = Color.yellow;
            
        }
        if (streakCounter >= 48)
        {

          
            multiplier = 4;
            bonusText.SetText("X4 Bonus!");
            bonusText.color = Color.magenta;
            scoreManager.scoreText.color = Color.magenta;
            activeWord.display.text.color = Color.magenta;
        }
    }




    
    public void showPointsPopUp()
    {
        //variable to hold word's points
        float wordPoints = (activeWord.word.Length * 10 * multiplier - (penalties * 10));

        var pointObj = Instantiate(scorePrefab, activeWord.display.transform.position, Quaternion.identity, wordCanvas);

        if(wordPoints > 0)
        {
            pointObj.GetComponent<TextMeshProUGUI>().text = "+" + (activeWord.word.Length * 10 * multiplier - (penalties * 10));
            pointObj.GetComponent<TextMeshProUGUI>().color = bonusText.color;
        }
        else if(wordPoints < 0)
        {
            pointObj.GetComponent<TextMeshProUGUI>().text = (activeWord.word.Length * 10 * multiplier - (penalties * 10)).ToString();
            pointObj.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            pointObj.GetComponent<TextMeshProUGUI>().text = "0";
            pointObj.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        
       
    }


    //main method for game typing
    public void TypeLetter (char letter)
    {
        if (hasActiveWord) //if were locked onto a word
        {
           if(isGameOver())
            {
                return;
            }
            //checking if letter typed is next letter and remove it from the word
            if(activeWord.GetNextLetter() == letter)
            {
                activeWord.Type();              
                GetMultiplier();              
            }
            else
            {
                if (multiplier != 1)
                {
                    bonusText.SetText("Streak Lost!");
                    bonusText.color = Color.red;
                    SoundManager.PlaySound("game_over_13");
                }


                activeWord.display.text.color = Color.red;
                streakCounter = 0;
                multiplier = 1;                        
                SoundManager.PlaySound("hit_24");
                penalties++;
                
            }
        }
        else //checking if the letter were typing is the first letter of a word, to lock on
        {
            if (isGameOver())
            {
                return;
            }
            foreach (Word word in words)
            {
                if(word.GetNextLetter() == letter)
                {
                    penalties = 0;
                    activeWord = word;
                    hasActiveWord = true; //There is now an active word
                    word.Type();                 
                    GetMultiplier();               
                    break;
                }
            }
        }

        //word is complete, and assign points to player
        if (hasActiveWord && activeWord.wordComplete())
        {
            if(penalties == 0)
            {
                streakCounter++;
                GetMultiplier();
            }

            if(streakCounter == 12 || streakCounter == 24 || streakCounter == 48)
            {
                SoundManager.PlaySound("power_up_03");
            }

            float wordPoints = (activeWord.word.Length * 10 * multiplier - (penalties * 10));
            scoreManager.scoreCount += wordPoints;
            showPointsPopUp();
            penalties = 0;
            hasActiveWord = false;         
            words.Remove(activeWord);
            SoundManager.PlaySound("hit_16");

            WordDisplay.IncreaseSpeed();
        }     
    }



    public bool isGameOver()
    {
        foreach (Word word in words)
        {
            if (word.display.playerLost)
            {         
               
                return true;
            }
        }
        return false;
    }


    
}
