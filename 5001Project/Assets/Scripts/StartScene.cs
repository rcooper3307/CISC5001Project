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
    string filePath, fileName;
    Vector2 position = new Vector2((float)-600.9695, (float)-408.0458);

    // Start is called before the first frame update
    void Start()
    {  
        if (pieceList == null)
        {
            pieceList = GameObject.FindGameObjectWithTag("pieceList");
        }
        setPieceList();
    }
    //reads file input and turns them into timeline piece objects
    void setPieceList()
    {
        fileName = "World_History.txt";
        filePath = Application.dataPath + "/FileInput/" + fileName;
        using (StreamReader sr = new StreamReader(filePath))
        {
            int series = 1;
            int value = 1;
            
            while (!sr.EndOfStream)
            {
                

                // Reads the next line
                string line = sr.ReadLine();

                // Split the string by commas
                List<string> items = line.Split(',').ToList();
                foreach(string s in items)
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

        }
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
        
        SceneManager.LoadScene("MainMenu");
    }
}
