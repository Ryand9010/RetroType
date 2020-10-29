using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawn : MonoBehaviour
{
    //word object that falls
    public GameObject wordPrefab;
 
    //canvas for the objects to fall against
    public Transform wordCanvas;

 


    public WordDisplay SpawnWord()
    {
        Vector3 randomPosition = new Vector3(Random.Range(93.4f, 106f), 109.2f, 0);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition ,Quaternion.identity, wordCanvas);
      
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }


}
