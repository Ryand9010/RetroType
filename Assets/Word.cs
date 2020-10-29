using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word{

    public string word;

    private int typeIndex; //indexer for a word

    public WordDisplay display;





    //constructor
    public Word(string _word, WordDisplay _display)
    {
        word = _word;
        typeIndex = 0; // set to 0
        display = _display;

        display.SetWord(word);
 
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void Type()
    {
        typeIndex++;
        display.RemoveLetter();     
    }


    //check to see if typeIndex has gone the length of the word, in which the word has been completely typed
    public bool wordComplete()
    {
        bool wordCompleted = (typeIndex >= word.Length);

        if(wordCompleted)
        {

            display.RemoveWord();

        }
        return wordCompleted;
    }







}
