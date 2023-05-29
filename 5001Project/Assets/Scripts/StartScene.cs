using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;


public class StartScene : MonoBehaviour
{
    [SerializeField] public InputField playerNameInput;
    [SerializeField] public InputField playerDateInput;
    [SerializeField] public GameObject pieceList;
    [SerializeField] public GameObject TimelineSegment;
    [SerializeField] public LevelLoader levelLoader;
    string filePath;
    string fileName;
    Vector2 position = new Vector2((float)-600.9695, (float)-408.0458);

    // Start is called before the first frame update
    void Start()
    {  
        if (pieceList == null)
        {
            pieceList = GameObject.FindGameObjectWithTag("pieceList");
        }
    }
    /*
      private List<string> TextAssetToList(TextAsset ta)
 {
     var listToReturn = new List<string>();
     var arrayString = ta.text.Split('\n');
     foreach (var line in arrayString)
     {
         listToReturn.Add(line);
     }
     return listToReturn;
 }
     */










    //reads file input and turns them into timeline piece objects
    void setPieceList()
    {
        //filePath = Application.dataPath + "/FileInput/" + fileName;
        //Resources.Load<TextAsset>(fileName)
        TextAsset ta = Resources.Load<TextAsset>(fileName);

        List<string> fileInput = ta.text.Split('\n').ToList();

        int series = 1;
        int value = 1;

        foreach(string line in fileInput)
        {
            List<string> items = line.Split(',').ToList();

            foreach (string s in items)
            {
                //Declare and initialize TimelinePiece and add it to PieceList
                GameObject p = Instantiate(TimelineSegment, position, Quaternion.identity);
                p.name = s;
                p.GetComponent<TimelinePiece>().setText(s);
                p.GetComponent<TimelinePiece>().setValue(value);
                p.GetComponent<TimelinePiece>().setSeries(series);
                p.transform.SetParent(pieceList.transform);
                value++;
            }
            series++;
        }

       /* while (!sr.EndOfStream)
        {


            // Reads the next line
            string line = sr.ReadLine();

            // Split the string by commas
            List<string> items = line.Split(',').ToList();
            foreach (string s in items)
            {
                //Declare and initialize TimelinePiece and add it to PieceList
                GameObject p = Instantiate(TimelineSegment, position, Quaternion.identity);
                p.name = s;
                p.GetComponent<TimelinePiece>().setText(s);
                p.GetComponent<TimelinePiece>().setValue(value);
                p.GetComponent<TimelinePiece>().setSeries(series);
                p.transform.SetParent(pieceList.transform);
                value++;
            }
            series++;


            //Add lists into list of lists
            //eventsArr.Add(items);
        }
        using (StreamReader sr = new StreamReader(Resources.Load<TextAsset>(fileName)))
        {
            

        }
       */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        string s = playerNameInput.text;
        
        PersistentData.Instance.SetName(s);
        /*
         * Commented out code to insert a value for the players name and date to be used as an object
        string d = playerDateInput.text;
        GameObject p = Instantiate(TimelineSegment, position, Quaternion.identity);
        p.name = s;
        p.GetComponent<TimelinePiece>().setText(s);
        p.GetComponent<TimelinePiece>().setValue(int.Parse(d));
        p.GetComponent<TimelinePiece>().setSeries(PersistentData.Instance.finalSeries);
        p.transform.SetParent(pieceList.transform);
        */
        
        levelLoader.LoadNextLevel("LevelSelect");
    }

    public void Set1()
    {
        fileName = "Option1";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set2()
    {
        fileName = "Option2";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set3()
    {
        fileName = "Option3";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set4()
    {
        fileName = "Option4";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }
}
