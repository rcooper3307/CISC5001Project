using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TimelineDisplay : MonoBehaviour
{
    [SerializeField] GameObject Timeline;
    [SerializeField] private List<GameObject> ElementsToPause;
    [SerializeField] private List<TimelinePiece> pieces;
    [SerializeField] private List<TimelinePiece> piecesSelected = new List<TimelinePiece>();
    [SerializeField] private Transform timelineSlots;
    [SerializeField] private Transform outOfDisplay;
    [SerializeField] public List<Text> textField;
    [SerializeField] public List<GameObject> previousSpots = new List<GameObject>();
    public GameObject pieceList;
    public int piecelistsize;
    public string titlestring;
    public int piecesPicked = 0;
    public int currentpage = 1;
    public int pageCount;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoRight()
    {
        if(currentpage != pageCount)
        {
            currentpage++;
            CreateTimeline();
        }
        else
        {
            Debug.Log("No more pages on the right!");
        }
    }

    public void GoLeft()
    {
        if(currentpage != 1)
        { 
            currentpage--;
            CreateTimeline();
        }
        else
        {
            Debug.Log("No more pages on the left!");
        }
    }

    public void DisplayTimeline()
    {
        CountPages();
        StoreLocation();
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(false);
        }
        Timeline.SetActive(true);
        Time.timeScale = 0f;

        CreateTimeline();
    }
    
    public void Return()
    {
        piecesSelected = new List<TimelinePiece>();
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(true);
        }
        Timeline.SetActive(false);
        LoadLocation();
        Time.timeScale = 1f;
    }
    
    public void StoreLocation()
    {
        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            
            GameObject placeholderTransform = new GameObject();
            placeholderTransform.transform.position = placeholder.transform.position;
            previousSpots.Add(placeholderTransform);
        }
    }

    public void LoadLocation()
    {
        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            placeholder.transform.position = previousSpots[i].transform.position;
        }

        foreach(GameObject obj in previousSpots)
        {
            Destroy(obj);
        }
        previousSpots.Clear();
    }

    public void CreateTimeline()
    {
         for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);

            placeholder.transform.position = outOfDisplay.position;
        }

        for (int i = 0; i < 6; i++)
        {
            textField[i].text = "";
        }

        for (int i = (currentpage-1)*6, j = 0; i < piecesSelected.Count && j < 6; i++, j++)
        {
            if (piecesSelected[i].isPicked())
            {
                switch(i%6)
                {
                    case 0:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(0).position;
                        textField[0].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    case 1:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(1).position;
                        textField[1].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    case 2:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(2).position;
                        textField[2].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    case 3:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(3).position;
                        textField[3].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    case 4:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(4).position;
                        textField[4].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    case 5:
                        piecesSelected[i].transform.position = timelineSlots.GetChild(5).position;
                        textField[5].text = piecesSelected[i].GetComponentInChildren<TextMeshPro>().text;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void CountPages()
    {
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;
        int piecesCounted = 0;
        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            if (placeholder.isPicked()) 
            {
                Select(placeholder);
                piecesCounted++;
            }
        }

        piecesPicked = piecesCounted;

        pageCount = (int)Mathf.Floor(piecesPicked/6);
        if (piecesPicked%6 != 0) pageCount++;
        Debug.Log(pageCount);
    }

    void Select(TimelinePiece p)
    {
        piecesSelected.Add(p);
    }
}

