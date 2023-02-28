using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class FileManager : MonoBehaviour
{
    string filePath, fileName;
    string[] eventsArr;
    void Start()
    {
        fileName = "World_History.txt";
        filePath = Application.dataPath + "/FileInput/" + fileName;
        readFile();
    }
    //I want to read the file by line, separating each item by a comma and then putting all those items into an array
    //Then, I want to put all the arrays into an array of arrays to be randomly selected from whenever the game begins
    //
    public void readFile()
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            while (!sr.EndOfStream)
            {
                // Reads the next line
                string line = sr.ReadLine();

                // Split the string by commas
                string[] items = line.Split(',');
                foreach(string s in items)
                    Debug.Log(s + ", ");
                Debug.Log("-------------------------------");
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
