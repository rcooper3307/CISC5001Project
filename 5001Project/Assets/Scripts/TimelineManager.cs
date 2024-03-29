using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private Transform shownSpawnLoc, pieceSpawnLoc, selectedLoc;
    [SerializeField] private List<TimelineSlot> slots;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    [SerializeField] PersistentData p;
    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public GameObject CorrectText;
    [SerializeField] public GameObject WrongText;
    
    ProgressScript progress;

    public GameObject pieceList;
    public int pieceSeries;
    public int[] givenvalues = new int[2];
    public int[] selectedIndex = new int[2];

    // Awake is called before the start
    void Awake()
    {
        InitializationProcess();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnSlots();
        SpawnPiece();
        p = FindObjectOfType<PersistentData>();
        progress = FindObjectOfType<ProgressScript>();
        if (levelLoader == null)
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }
        CorrectText.SetActive(false);
        WrongText.SetActive(false);
    }
    
    
    //Initial Slot Spawner
    void SpawnSlots()
    {
        //Assigns piece values to givenvalues array and indexes to selectedIndex array
        piecesSelected[0].transform.position = shownSpawnLoc.GetChild(0).position;
        piecesSelected[1].transform.position = shownSpawnLoc.GetChild(1).position;
        Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);
    }

    //Selects an unselected piece to put into play
    void SpawnPiece()
    {
        Select(pieces[0]);

        piecesSelected[2].transform.position = pieceSpawnLoc.position;
        piecesSelected[2].activate();
        piecesSelected[2].newPos();
        
        int value = piecesSelected[2].getValue();
        Debug.Log("VALUE OF CURRENT PIECE " + value);

        if (value > givenvalues[1])
            piecesSelected[2].Init(slots[2]);
        else if (value < givenvalues[0])
            piecesSelected[2].Init(slots[0]);
        else
            piecesSelected[2].Init(slots[1]);
    }

    void Select(TimelinePiece p)
    {
        piecesSelected.Add(p);
        pieces.Remove(p);
    }
  
    public void CleanUp()
    {
        CorrectText.SetActive(false);
        piecesSelected[2].deactivate();
        piecesSelected[2].transform.position = selectedLoc.position;
        piecesSelected[0].transform.position = selectedLoc.position;
        piecesSelected[1].transform.position = selectedLoc.position;

        piecesSelected[2].pickPiece(); 
    }

    public IEnumerator WrongTextAppears()
    {
        WrongText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        WrongText.SetActive(false);
    }

    public void Proceed()
    {
        //Post Game Section
        if (!PersistentData.Instance.GameStatus())
        {
            progress.AddProgress();
            StartCoroutine(SceneDelay("PickOne",3f));
        }
        else
        {
            progress.AddProgress();
            for (int i = 0; i < pieceList.transform.childCount; i++)
                pieceList.GetComponentInChildren<TimelinePiece>(i).releasePiece();
            PersistentData.Instance.GameUndone();
            PersistentData.Instance.ResetGame();
            StartCoroutine(SceneDelay("Final", 3f));
        }
            
    }
    //delays the transition to scene 'scene' by float 'time'
    IEnumerator SceneDelay(string scene, float time)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //if the "wrong" text is on the screen currently, make it invisible and activate the correct text
        if (WrongText.activeSelf)
        {
            WrongText.SetActive(false);
        }
        CorrectText.SetActive(true);
        piecesSelected[2].deactivate();
        //yield on a new YieldInstruction that waits for time seconds.
        yield return new WaitForSeconds(time);

        CleanUp();

        levelLoader.LoadNextLevel(scene);
        //SceneManager.LoadScene(scene);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        // Code to execute after 3 second delay
    }
    
    public void InitializationProcess()
    {
        int piecelistsize = 0;
        TimelinePiece placeholder;

        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        givenvalues[0] = PersistentData.Instance.GetPieceOne();
        givenvalues[1] = PersistentData.Instance.GetPieceTwo();
        pieceSeries = PersistentData.Instance.GetSeries();

        for (int i = 0; i < piecelistsize; i++)
        {
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            if (placeholder.getSeries() == pieceSeries)
                pieces.Add(placeholder);
        }

        int listsize = pieces.Count;
        int[] selectedIndex = new int[2];

        for(int i = 0; i < listsize; i++)
        {
            Debug.Log(pieces[i].getValue());
            if (pieces[i].getValue() == givenvalues[0] )
            {
                Debug.Log("COND 1");  
                selectedIndex[0] = i;  
            }
            else if (pieces[i].getValue() == givenvalues[1])
            {
                Debug.Log("COND 2");  
                selectedIndex[1] = i;    
            }
        }
        
        Select(pieces[selectedIndex[0]]);
        if (selectedIndex[0] > selectedIndex[1])
            Select(pieces[selectedIndex[1]]);
        else
            Select(pieces[selectedIndex[1]-1]);
    }
}

public static class ExtensionFunction
{
    public static T GetComponentInChildren<T>(this GameObject gameObject, int index)
    {
        return gameObject.transform.GetChild(index).GetComponent<T>();
    }
}