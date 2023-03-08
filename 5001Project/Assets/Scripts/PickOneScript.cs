using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOneScript : MonoBehaviour
{
    [SerializeField] private Transform shownSpawnLoc, selectedLoc, buttonLoc;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    private int pairindex = 0;
    public GameObject pieceList;
    public int[] givenvalues = new int[2];
    

    void Awake()
    {
        int piecelistsize = 0;
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            pieces.Add(placeholder);
        }

        SpawnSlots();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Initial Slot Spawner
    void SpawnSlots()
    {
        //Assigns piece values to givenvalues array
        for (int i = 0; i < 2; i++)
        {
            int listsize = pieces.Count;
            int random = Random.Range(0,listsize-1);

            givenvalues[i] = pieces[random].getValue();
            pieces[random].transform.position = shownSpawnLoc.GetChild(i).position;

            Select(pieces[random]);
        }

        bool LGR = true;
        
        if(givenvalues[0] > givenvalues[1]) //condition of value of index 0 piece > index 1 piece 
        {
            GameObject correct = GameObject.Find("CorrectChoice");
            correct.transform.position = buttonLoc.GetChild(1).position;

            GameObject wrong = GameObject.Find("WrongChoice");
            wrong.transform.position = buttonLoc.GetChild(0).position;

            Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);

            LGR = true;
        }
        else //vice versa
        {
            GameObject correct = GameObject.Find("CorrectChoice");
            correct.transform.position = buttonLoc.GetChild(0).position;

            GameObject wrong = GameObject.Find("WrongChoice");
            wrong.transform.position = buttonLoc.GetChild(1).position;

            Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);

            LGR = false;
        }
    }

    void Restart()
    {
        //Reset Section
        piecesSelected[pairindex].transform.position = selectedLoc.position;
        piecesSelected[pairindex+1].transform.position = selectedLoc.position;

        pairindex += 2;

        //Respawn Section
        SpawnSlots();
    }

    void Select(TimelinePiece p)
    {
        piecesSelected.Add(p);
        pieces.Remove(p);
    }

    public void SelectWrong()
    {
        Debug.Log("Wrong");
    }

    public void SelectRight()
    {
        Debug.Log("Right");
        Restart();
    }
}
