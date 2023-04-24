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
    [SerializeField] private Transform timelineSlots;
    [SerializeField] private Transform outOfDisplay;
    [SerializeField] public List<Text> textField;
    [SerializeField] public List<GameObject> previousSpots = new List<GameObject>();
    public GameObject pieceList;
    public int piecelistsize;
    public string titlestring;
    public int seriesCount;
    public int currentpage = 1;
    public int pageCount;

    void Awake()
    {
        StoreLocation();
        LoadLocation();
        StoreLocation();
        LoadLocation();
    }

    // Start is called before the first frame update
    void Start()
    {
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        TimelinePiece placeholder;
        placeholder = pieceList.GetComponentInChildren<TimelinePiece>(piecelistsize - 1);
        seriesCount = placeholder.getSeries();
        pageCount = seriesCount;
        Debug.Log(pageCount);
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
        Time.timeScale = 1f;
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(true);
        }
        Timeline.SetActive(false);
        LoadLocation();
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

        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            if (placeholder.isPicked())
            {
                if (placeholder.getSeries() == currentpage)
                {
                    switch(i%3)
                    {
                        case 0:
                            placeholder.transform.position = timelineSlots.GetChild(0).position;
                            textField[0].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        case 1:
                            placeholder.transform.position = timelineSlots.GetChild(1).position;
                            textField[1].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        case 2:
                            placeholder.transform.position = timelineSlots.GetChild(2).position;
                            textField[2].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        default:
                            break;
                    }
                }
                else if (placeholder.getSeries() == currentpage + 1)
                {
                    switch(i%3)
                    {
                        case 0:
                            placeholder.transform.position = timelineSlots.GetChild(3).position;
                            textField[3].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        case 1:
                            placeholder.transform.position = timelineSlots.GetChild(4).position;
                            textField[4].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        case 2:
                            placeholder.transform.position = timelineSlots.GetChild(5).position;
                            textField[5].text = placeholder.GetComponentInChildren<TextMeshPro>().text;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
