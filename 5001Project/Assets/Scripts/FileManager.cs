using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;


public class FileManager : MonoBehaviour
{
    string filePath, fileName;
    [SerializeField] public List<List<string>> eventsArr = new List<List<string>>();
    [SerializeField] int test = 0;
    [SerializeField] GameObject QuestionPiece1;
    [SerializeField] GameObject QuestionPiece2;
    [SerializeField] GameObject DragPiece;
    //[SerializeField] Text QP1textbox = QuestionPiece1.GetComponentInChildren<Text>();
    //[SerializeField] Text QP2textbox = QuestionPiece2.GetComponentInChildren<Text>();
    //[SerializeField] Text DPtextbox  = DragPiece.GetComponentInChildren<Text>();


    //if i wanted a method to return an array of arrays, i'd have to give it a return type
    //what i want right now is not to declare 9 billion arrays from the start, i want to only build arrays as i read lines from the file 
    //and then i want to add the arrays and the items in them to my already existing array of arrays, eventsArr


    void Start()
    {
        fileName = "World_History.txt";
        filePath = Application.dataPath + "/FileInput/" + fileName;
        readFile();
        printLists();
    }
    //I want to read the file by line, separating each item by a comma and then putting all those items into an array
    //Then, I want to put all the arrays into an array of arrays to be randomly selected from whenever the game begins
    //use lists instead of arrays
    public void readFile()
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            
            while (!sr.EndOfStream)
            {
                // Reads the next line
                string line = sr.ReadLine();

                // Split the string by commas
                List<string> items = line.Split(',').ToList();
                //Add lists into list of lists
                eventsArr.Add(items);
                
                /*
                foreach (string s in items)
                    Debug.Log(s + ", ");
                Debug.Log("-------------------------------");
                */
            }
           
        }
    }

    void printLists()
    {
        foreach(List<string> list in eventsArr)
        {
            string line = "";
            foreach(string s in list)
            {
                line += s + " ";
            }
            Debug.Log(line);
        }
        test++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
