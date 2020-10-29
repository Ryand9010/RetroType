using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadTextFiles : MonoBehaviour
{

    public  TextAsset easy;
    public  TextAsset medium;
    public  TextAsset hard;

    public static List<string> easyList = new List<string>();
    public static List<string> mediumList = new List<string>();
    public static List<string> hardList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        ReadLists(easy, easyList);
        ReadLists(medium, mediumList);
        ReadLists(hard, hardList);
    }



    public void ReadLists(TextAsset doc, List<string> list)
    {
        string line;
        StringReader lineReader = new StringReader(doc.text);

        while ((line = lineReader.ReadLine()) != null)
        {
            //add each word from text file into the list
            list.Add(line);
        }
        lineReader.Close();
    }
}
