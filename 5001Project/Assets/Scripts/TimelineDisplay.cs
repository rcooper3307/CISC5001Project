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
    [SerializeField] private Transform displayLoc, outofFrameLoc;
    [SerializeField] private List<TimelinePiece> pieces;
    public GameObject pieceList;
    public int piecelistsize;
    public int currentpiece;
    public Text titleField;
    public string titlestring;

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
        if(currentpiece != piecelistsize - 1)
        {
            currentpiece++;
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring; 
        }
        else
        {
            Debug.Log("There is nothing on the right of this piece!");
        }
    }

    public void GoLeft()
    {
        if(currentpiece != 0)
        { 
            currentpiece--;
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring;
        }
        else
        {
            Debug.Log("There is nothing on the left of this piece!");
        }
    }

    public void DisplayTimeline()
    {
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(false);
        }
        Timeline.SetActive(true);
        Time.timeScale = 0f;

        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        currentpiece = 0;

        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            if (placeholder.isPicked())
                pieces.Add(placeholder);
        }

        piecelistsize = pieces.Count;

        titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
        titleField.text = titlestring;

        pieceList.SetActive(false);
    }
    
    public void Return()
    {
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(true);
        }
        Timeline.SetActive(false);
        Time.timeScale = 1f;

        pieceList.SetActive(true);
    }
}
