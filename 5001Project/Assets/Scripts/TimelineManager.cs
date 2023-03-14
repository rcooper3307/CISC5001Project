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
    [SerializeField] private FileManager fm;
    public GameObject pieceList;
    public int[] givenvalues = new int[2];
    public int[] selectedIndex = new int[2];
    public bool LGR = false;
    public final int NUM_OF_PIECES = 2;
    private int row = 0;
    private int column = 0;
    private final int LEFT_PIECE = 0;
    private final int RIGHT_PIECE = 1;
    private final int DRAG_PIECE = 2;


    // Awake is called before the start
    void Awake()
    {
        //grabs the piecelist object and makes piecelistsize = to the amount of children it has
        int piecelistsize = 0;
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;
        //adds all pieces from piecelist into the pieces array, which is a list made of timeline pieces 
        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            pieces.Add(placeholder);
        }
        //listsize = 12
        //random = random from 0 to 11
        //selects 2 random pieces and it to the piecesselected array and removes it from the pieces array

        //instead of intializing these two manually, there should be a method to do it for me.
        for (int i = 0; i < NUM_OF_PIECES; i++)
        {
            //int listsize = pieces.Count;
            //int random = Random.Range(0, listsize - 1);
            column = Random.Range(0,fm.eventsArr[row].Length-1)
            Select(pieces[i],row,column); 
            //checks t
            if(pieces[i].GetComponent<TimelinePiece>().text.text = fm.)
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
        //assigns givenvalues[i] the value of pieceSelected[i]'s value
        givenvalues[0] = piecesSelected[0].getValue();
        //assigns the value of selectedIndex[0] to be 0
        selectedIndex[0] = 0;
        //assigns givenvalues[i+1] the value of pieceSelected[i+1]'s value
        givenvalues[1] = piecesSelected[1].getValue();
        //assigns the value of selectedIndex[1] to be 1
        selectedIndex[1] = 1;

        if (givenvalues[1] > givenvalues[0])
        //if givenvalue[1] (the value of the 1st piece in piecesselected) > givenvalue[0] (the value of the 2nd piece in piecesselected)
        {
            //the sprite for pieceSelected[0] is moved to the left position
            piecesSelected[0].transform.position = shownSpawnLoc.GetChild(0).position;
            //the sprite for pieceSelected[1] is moved to the right position
            piecesSelected[1].transform.position = shownSpawnLoc.GetChild(1).position;
            Debug.Log("LEFT: " + givenvalues[0] + "  RIGHT: " + givenvalues[1]);
            //dunno what this means yet
            LGR = false;
        }
        else //if givenvalue[1] (the value of the 1st piece in piecesselected) < givenvalue[0] (the value of the 2nd piece in piecesselected)
        {
            //the sprite for pieceSelected[0] is moved to the right position
            piecesSelected[0].transform.position = shownSpawnLoc.GetChild(1).position;
            //the sprite for pieceSelected[1] is moved to the left position
            piecesSelected[1].transform.position = shownSpawnLoc.GetChild(0).position;
            Debug.Log("LEFT: " + givenvalues[1] + "  RIGHT: " + givenvalues[0]);
            LGR = true;
        }
    }

    //instead of having given values, there should just be something to see what the values are in the left and right spots at the current moment.


    //Selects an unselected piece to put into play
    void SpawnPiece()
    {
        //listsize= the number of pieces currently in pieces
        int listsize = pieces.Count;
        //selects a random number from 0 to the size of the list-1
        int random = Random.Range(0, listsize - 1);
        //selects 1 random piece and adds it to the piecesselected array and removes it from the pieces array
        Select(pieces[random]);
        //the selected piece is always going to be the last piece in the piecesSelected array, so its pieceindex = the size of piecesSelected-1
        int pieceindex = (piecesSelected.Count) - 1;
        //sets the position of the draggable piece to the spawning location spot.
        piecesSelected[pieceindex].transform.position = pieceSpawnLoc.position;
        //makes the piece draggable
        piecesSelected[pieceindex].activate();
        //sets the origin position of this piece to its current spawn
        piecesSelected[pieceindex].newPos();

        //value equals the value of the draggable piece
        int value = piecesSelected[pieceindex].getValue();
        Debug.Log("VALUE OF CURRENT PIECE " + value);

        //if?? givenvalue0 is greater than givenvalue1

        //(val0 is on the right and val 1 is one the left)
        if (LGR) //condition of givenvalue[0] > givenvalue[1]
        {
            //if the draggable value is greater than givenvalue0
            if (value > givenvalues[0])
                //a slot is initialized for the draggable piece on the far right slot 
                piecesSelected[pieceindex].Init(slots[2]);
            //if value is less than givenvalue1
            else if (value < givenvalues[1])
                //a slot is initialized for the draggable piece on the far left slot
                piecesSelected[pieceindex].Init(slots[0]);
            //else, a slot is initialized in the middle
            else
                piecesSelected[pieceindex].Init(slots[1]);
        }
        //if givenvalue1 is on the right and givenvalue0 is on the left
        else //condition of givenvalue[1] > givenvalue[0]
        {
            //if draggable piece's value is greater than the value on the right
            if (value > givenvalues[1])
                //its slot is initialized in slot2 on the far right
                piecesSelected[pieceindex].Init(slots[2]);
            //if DP value is greater than the value on the left
            else if (value < givenvalues[0])
                //its slot is initialized in slot1 on the far left
                piecesSelected[pieceindex].Init(slots[0]);
            else
                //slot is initialized in the middle
                piecesSelected[pieceindex].Init(slots[1]);
        }
    }
    //adds a piece to the piecesselected array and removes it from the pieces array
    void Select(TimelinePiece p, int row, int column)
    {
        p.GetComponent<TimelinePiece>().text.text = fm.eventsArr[row][column];
        p.GetComponent<TimelinePiece>().setValue(column);
        //piecesSelected.Add(p);
        //pieces.Remove(p);
    }




    /*void SpawnNewPieces()
    {
        for (int i = 0; i < NUM_OF_PIECES; i++)
        {
            //int listsize = pieces.Count;
            //int random = Random.Range(0, listsize - 1);
            column = Random.Range(0, fm.eventsArr[row].Length - 1)
            Select(pieces[i], row, column);
            //checks t
            if (pieces[i].GetComponent<TimelinePiece>().text.text = fm.eventsArr)
        }
    }
    */
    //every time a piece matches into the correct slot, restart is run
    public void Restart()
    {
        //Reset Section
        ///////
        //sets the index to the index of the draggable piece
        int pieceindex = (piecesSelected.Count) - 1;
        //makes the draggable piece undraggable
        piecesSelected[pieceindex].deactivate();
        //moves that piece off the screen to the left
        piecesSelected[pieceindex].transform.position = selectedLoc.position;
        //moves the other two pieces to the same location
        piecesSelected[selectedIndex[0]].transform.position = selectedLoc.position;
        piecesSelected[selectedIndex[1]].transform.position = selectedLoc.position;

        //Restart Section


        //searches through the list of pieces until there are no more pieces to select from
        if (row < fm.eventsArr.Length) //checks to make sure that we haven't reached the end of the list of fileinput
        {
            //randomly selects two indexes from selected list
            //listsize=3
            //rand1 = random from 0 to listsize-1
            //rand2 = random from 0 to listsize-1 that is diff from rand1

            //randomly selects indexes from the current row
            int listsize = pieces.Count;
            for(int i = 0; i < listsize; i++)
            {
                Select(pieces[i], row, i);
                pieces[i].setValue(i);
            }
            /////////////////////////////THIS IS WHERE I WAS LAST WORKING.
            ///
            int rand1 = Random.Range(0, listsize - 1);
            int rand2 = Random.Range(0, listsize - 1);
            while (rand1 == rand2) //to make sure rand1 != rand2
                rand2 = Random.Range(0, listsize - 1);
            //i need to
            Select(pieces[FIRST], row, rand1);


            //trying to implement the thing
            


            ////////////////////////////////////////////////////////

            //assigns the two index selected to selectedIndex array and their values to givenvalues array

            //the value of the new number is put in givenval0
            givenvalues[0] = piecesSelected[rand1].getValue();
            //assigns selectedIndex[0] the value of rand 1, a random index from 0 to the size of the array of pieces we have already selected
            selectedIndex[0] = rand1;

            //vice versa
            givenvalues[1] = piecesSelected[rand2].getValue();
            selectedIndex[1] = rand2;

            //if the second val in givenval is greater than the first
            if (givenvalues[1] > givenvalues[0]) //condition of value of index 1 piece > index 0 piece 
            {
                //it gets placed on the right while the lesser value is on the left
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
            ///////////////////////////////////////////////////////

            //calls SpawnPiece to select new piece to place on timeline
            SpawnPiece();
            row++;
        }
        else //condition for when all the pieces are selected
        {
            Debug.Log("FINISHED");

        }
    }
}

public static class ExtensionFunction
{
    public static T GetComponentInChildren<T>(this GameObject gameObject, int index)
    {
        return gameObject.transform.GetChild(index).GetComponent<T>();
    }
}
