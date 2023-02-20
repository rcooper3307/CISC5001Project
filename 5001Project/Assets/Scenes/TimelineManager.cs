using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private Transform shownSpawnLoc, pieceSpawnLoc, selectedLoc;
    [SerializeField] private List<TimelineSlot> slots;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    public int[] givenvalues = new int[2];
    public int[] selectedIndex = new int[2];
    public bool LGR = false;

    // Awake is called before the start
    void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            int listsize = pieces.Count;
            int random = Random.Range(0,listsize-1);
            Select(pieces[random]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnSlots();
        SpawnPiece();
    }
    
    //Initial Slot Spawner
    void SpawnSlots()
    {
        //Assigns piece values to givenvalues array and indexes to selectedIndex array
        givenvalues[0] = piecesSelected[0].getValue();
        selectedIndex[0] = 0;

        givenvalues[1] = piecesSelected[1].getValue();
        selectedIndex[1] = 1;

        if(givenvalues[1] > givenvalues[0]) //condition of value of index 1 piece > index 0 piece 
        {
            piecesSelected[0].transform.position = shownSpawnLoc.GetChild(0).position;
            piecesSelected[1].transform.position = shownSpawnLoc.GetChild(1).position;
            Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);
            LGR = false;
        }
        else //vice versa
        {
            piecesSelected[0].transform.position = shownSpawnLoc.GetChild(1).position;
            piecesSelected[1].transform.position = shownSpawnLoc.GetChild(0).position;
            Debug.Log("LEFT: " + givenvalues[1] + "  RIGHT: " + givenvalues[0]);
            LGR = true;
        }
    }

    //Selects an unselected piece to put into play
    void SpawnPiece()
    {
        int listsize = pieces.Count;
        int random = Random.Range(0,listsize-1);
        Select(pieces[random]);

        int pieceindex = (piecesSelected.Count)-1;
        piecesSelected[pieceindex].transform.position = pieceSpawnLoc.position;
        piecesSelected[pieceindex].activate();
        piecesSelected[pieceindex].newPos();

        int value = piecesSelected[pieceindex].getValue();
        Debug.Log("VALUE OF CURRENT PIECE " + value);

        if(LGR) //condition of givenvalue[0] > givenvalue[1]
        {
            if (value > givenvalues[0])
                piecesSelected[pieceindex].Init(slots[2]);
            else if (value < givenvalues[1])
                piecesSelected[pieceindex].Init(slots[0]);
            else
                piecesSelected[pieceindex].Init(slots[1]);
        }
        else //condition of givenvalue[1] > givenvalue[0]
        {
            if (value > givenvalues[1])
                piecesSelected[pieceindex].Init(slots[2]);
            else if (value < givenvalues[0])
                piecesSelected[pieceindex].Init(slots[0]);
            else
                piecesSelected[pieceindex].Init(slots[1]);
        }
    }

    void Select(TimelinePiece p)
    {
        piecesSelected.Add(p);
        pieces.Remove(p);
    }

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
}