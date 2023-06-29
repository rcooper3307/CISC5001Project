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
    string fileNameDates;
    string fileNameOrders;
    Vector2 position = new Vector2((float)-600.9695, (float)-408.0458);

    // Start is called before the first frame update
    void Start()
    {  
        if (pieceList == null)
        {
            pieceList = GameObject.FindGameObjectWithTag("pieceList");
        }
    }

    //reads file input and turns them into timeline piece objects
    void setPieceList()
    {
        TextAsset ta = Resources.Load<TextAsset>(fileName);
        TextAsset taD = Resources.Load<TextAsset>(fileNameDates);
        TextAsset taO = Resources.Load<TextAsset>(fileNameOrders);

        List<string> fileInput = ta.text.Split('\n').ToList();
        List<string> dates = taD.text.Split(',').ToList();
        List<string> orderString = taO.text.Split(',').ToList();

        int series = 1;
        int value = 1;

        foreach (string line in fileInput)
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
                p.GetComponent<TimelinePiece>().setYear(dates[value-1]);
                p.GetComponent<TimelinePiece>().setOrder(int.Parse(orderString[value-1]));
                p.transform.SetParent(pieceList.transform);
                value++;
            }
            series++;
        }
    }


    public void PlayGame()
    {
        string s = playerNameInput.text;
        string d = playerDateInput.text;
        
        PersistentData.Instance.SetName(s);
        PersistentData.Instance.SetDate(d);
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
        fileNameDates = "Option1Dates";
        fileNameOrders = "Option1Order";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set2()
    {
        fileName = "Option2";
        fileNameDates = "Option2Dates";
        fileNameOrders = "Option2Order";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set3()
    {
        fileName = "Option3";
        fileNameDates = "Option3Dates";
        fileNameOrders = "Option3Order";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Set4()
    {
        fileName = "Option4";
        fileNameDates = "Option4Dates";
        fileNameOrders = "Option4Order";
        setPieceList();
        levelLoader.LoadNextLevel("MainMenu");
    }
}
