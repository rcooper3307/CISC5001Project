using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickOneScript : MonoBehaviour
{
    [SerializeField] private Transform shownSpawnLoc, selectedLoc, buttonLoc;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    [SerializeField] PersistentData p;
    [SerializeField] public LevelLoader levelLoader;
    ProgressScript progress;
    
    
    public GameObject pieceList;
    public int pieceSeries;
    public int[] givenvalues = new int[2];
    public int[] indexValues = new int[2];
    public bool LGR = true;

    void Awake()
    {
        InitializationProcess();
        SpawnSlots();
    }
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<PersistentData>();
        progress = FindObjectOfType<ProgressScript>();
        if (levelLoader == null)
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }
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
        indexValues[0] = random;

        givenvalues[0] = pieces[random].getValue();
        pieceSeries = pieces[random].getSeries();

        pieces[random].transform.position = shownSpawnLoc.GetChild(0).position;
            
        Select(pieces[random]);

        //Assigns another piece with the same series value
        listsize = pieces.Count;
        do
        {
            random = Random.Range(0,listsize-1);

        }while(pieces[random].getSeries() != pieceSeries);

        indexValues[1] = random;
        givenvalues[1] = pieces[random].getValue();

        pieces[random].transform.position = shownSpawnLoc.GetChild(1).position;
            
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

    public void CleanUp()
    {
        int pieceindex = (piecesSelected.Count)-1;

        piecesSelected[pieceindex].pickPiece();
        piecesSelected[pieceindex-1].pickPiece();

        piecesSelected[pieceindex].transform.position = selectedLoc.position;
        piecesSelected[pieceindex-1].transform.position = selectedLoc.position;
    }

    void Proceed()
    {
        //increases progress bar
        progress.AddProgress();
        //Loads Next Scene

        StartCoroutine(SceneDelay("Menu",3f));
    }

    //delays the transition to scene 'scene' by float 'time'
    IEnumerator SceneDelay(string scene, float time)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        //CleanUp();

        //Selects the last piece in the series
        int listsize = pieces.Count;
        for (int i = 0; i < listsize; i++)
        {
            if (pieces[i].getSeries() == pieceSeries)
            {
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
        levelLoader.LoadNextLevel(scene);
        //SceneManager.LoadScene(scene);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        
    }

    void Select(TimelinePiece p)
    {
        piecesSelected.Add(p);
        pieces.Remove(p);
    }
     //plays the wrong noise
    public void SelectWrong()
    {
        p.Wrong();
        Debug.Log("Wrong");

    }
    //plays the right noise
    public void SelectRight()
    {
        p.Correct();
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
