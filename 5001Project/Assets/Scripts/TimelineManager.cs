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
        piecesSelected[2].deactivate();
        piecesSelected[2].transform.position = selectedLoc.position;
        piecesSelected[0].transform.position = selectedLoc.position;
        piecesSelected[1].transform.position = selectedLoc.position;

        piecesSelected[2].pickPiece(); 
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
            StartCoroutine(SceneDelay("MainMenu", 3f));
            for (int i = 0; i < pieceList.transform.childCount; i++)
                pieceList.GetComponentInChildren<TimelinePiece>(i).releasePiece();
            PersistentData.Instance.GameUndone();
        }
            
    }
    //delays the transition to scene 'scene' by float 'time'
    IEnumerator SceneDelay(string scene, float time)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        CleanUp();

        levelLoader.LoadNextLevel(scene);
        //SceneManager.LoadScene(scene);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        // Code to execute after 3 second delay
    }

    /*
        public void Restart()
        {

            //Reset Section
            int pieceindex = (piecesSelected.Count)-1;

            piecesSelected[pieceindex].deactivate();
            piecesSelected[pieceindex].transform.position = selectedLoc.position;
            piecesSelected[selectedIndex[0]].transform.position = selectedLoc.position;
            piecesSelected[selectedIndex[1]].transform.position = selectedLoc.position;

            //Restart Section
            if (pieces.Count > 0) //checks if there are any unselected pieces left
            {
                //randomly selects two indexes from selected list
                int listsize = piecesSelected.Count;
                int rand1 = Random.Range(0,listsize-1);
                int rand2 = Random.Range(0,listsize-1);
                while(rand1 == rand2) //to make sure rand1 != rand2
                    rand2 = Random.Range(0,listsize-1);

                //assigns the two index selected to selectedIndex array and their values to givenvalues array
                givenvalues[0] = piecesSelected[rand1].getValue();
                selectedIndex[0] = rand1;

                givenvalues[1] = piecesSelected[rand2].getValue();
                selectedIndex[1] = rand2;

                if(givenvalues[1] > givenvalues[0]) //condition of value of index 1 piece > index 0 piece 
                {
                    piecesSelected[rand1].transform.position = shownSpawnLoc.GetChild(0).position;
                    piecesSelected[rand2].transform.position = shownSpawnLoc.GetChild(1).position; 
                    Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);
                    LGR = false;
                }
                else //vice versa
                {
                    piecesSelected[rand1].transform.position = shownSpawnLoc.GetChild(1).position;
                    piecesSelected[rand2].transform.position = shownSpawnLoc.GetChild(0).position;
                    Debug.Log("LEFT: " + givenvalues[1] + "  RIGHT: " + givenvalues[0]);
                    LGR = true;
                }

                //calls SpawnPiece to select new piece to place on timeline
                SpawnPiece();
            }
            else //condition for when all the pieces are selected
            {
                Debug.Log("FINISHED");
            }
        }
    */
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