using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{

    static int randomIndex;
    static string randomWord;

    public ReadTextFiles readTextFiles;

    private static List<string> easyWordList = ReadTextFiles.easyList;
    private static List<string> mediumWordList = ReadTextFiles.mediumList;
    private static List<string> hardWordList = ReadTextFiles.hardList;


    public static string GetRandomWord ()
    {
        
        if(PlayerPrefs.GetString("difficulty") == "easy")
        {
            randomIndex = Random.Range(0, easyWordList.Count -1);         
            randomWord = easyWordList[randomIndex];
            
            return randomWord;
        }
        if (PlayerPrefs.GetString("difficulty") == "medium")
        {
            randomIndex = Random.Range(0, mediumWordList.Count);
            randomWord = mediumWordList[randomIndex];

            return randomWord;
            
        }
        if (PlayerPrefs.GetString("difficulty") == "hard")
        {
            randomIndex = Random.Range(0, hardWordList.Count);
            randomWord = hardWordList[randomIndex];

            return randomWord;
        }
        else
        {
            return null;
        }

       
    }
}
