using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class WordDisplay : MonoBehaviour
{
    public TextMeshPro text;

    public static float fallSpeed = 1f;

    //private float interval = 10f;

    public bool playerLost = false;



    public void SetWord (string word)
    {
        text.text = word;
    }

    public void RemoveLetter ()
    {
        text.text = text.text.Remove(0, 1);
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
     
    }


   

    private void Update()
    {   
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);

        //if ((int)Time.time % (int)interval == 0)
        //{
        //    IncreaseSpeed();
        //}


        if (text.transform.position.y <= 94.75f)
        {
            playerLost = true;
            Time.timeScale = 0f;       
        }

        
    }

    public static void IncreaseSpeed()
    {
        fallSpeed += 0.025f;
    }

    
}
