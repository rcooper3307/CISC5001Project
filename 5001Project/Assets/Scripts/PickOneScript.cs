using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickOneScript : MonoBehaviour
{
    [SerializeField] private Transform shownSpawnLoc, selectedLoc, buttonLoc;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    public GameObject pieceList;
    public int pieceSeries;
    public int[] givenvalues = new int[2];
    public bool LGR = true;

    void Awake()
    {
        InitializationProcess();
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
        //Assigns first piece and sets series value
        int listsize = pieces.Count;
        int random = Random.Range(0,listsize-1);

        givenvalues[0] = pieces[random].getValue();
        pieceSeries = pieces[random].getSeries();

        pieces[random].transform.position = shownSpawnLoc.GetChild(0).position;
        pieces[random].pickPiece();
            
        Select(pieces[random]);

        //Assigns another piece with the same series value
        listsize = pieces.Count;
        do
        {
            random = Random.Range(0,listsize-1);

        }while(pieces[random].getSeries() != pieceSeries);

        givenvalues[1] = pieces[random].getValue();

        pieces[random].transform.position = shownSpawnLoc.GetChild(1).position;
        pieces[random].pickPiece();
            
        Select(pieces[random]);
        
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
        Debug.Log(LGR);
    }

    void Proceed()
    {
        //Cleanup Section
        int pieceindex = (piecesSelected.Count)-1;

        piecesSelected[pieceindex].transform.position = selectedLoc.position;
        piecesSelected[pieceindex-1].transform.position = selectedLoc.position;

        //Selects the last piece in the series
        int listsize = pieces.Count;
        for(int i = 0; i < listsize; i++)
        {
            if (pieces[i].getSeries() == pieceSeries)
            {
                pieces[i].pickPiece();     
                Select(pieces[i]);
                break;
            }
        }

        //Sets persistent data for next scene
        PersistentData.Instance.SetSeries(pieceSeries);

        if (LGR)
            PersistentData.Instance.SetPieces(givenvalues[1], givenvalues[0]);
        else
            PersistentData.Instance.SetPieces(givenvalues[0], givenvalues[1]);

        if (pieces.Count < 1)
            PersistentData.Instance.GameDone();

        //Loads Next Scene
        SceneManager.LoadScene("Menu");
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
        Proceed();
    }

    public void InitializationProcess()
    {
        int piecelistsize = 0;
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            if (placeholder.isPicked())
                Select(placeholder);
            else
                pieces.Add(placeholder);
        }
    }
}
