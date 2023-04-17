using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LibraryShowCase : MonoBehaviour
{
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
        InitializationProcess();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoRight()
    {
        if(currentpiece != piecelistsize - 1)
        {
            pieces[currentpiece].transform.position = outofFrameLoc.position;
            pieces[++currentpiece].transform.position = displayLoc.position;  

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
            pieces[currentpiece].transform.position = outofFrameLoc.position;
            pieces[--currentpiece].transform.position = displayLoc.position;   
            
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring;
        }
        else
        {
            Debug.Log("There is nothing on the left of this piece!");
        }
    }


    public void InitializationProcess()
    {
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        piecelistsize = pieceList.transform.childCount;

        currentpiece = 0;

        for (int i = 0; i < piecelistsize; i++)
        {
            TimelinePiece placeholder;
            placeholder = pieceList.GetComponentInChildren<TimelinePiece>(i);
            pieces.Add(placeholder);
        }

        piecelistsize = pieces.Count;
        pieces[currentpiece].transform.position = displayLoc.position;

        titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
        titleField.text = titlestring;
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pieces[currentpiece].transform.position = outofFrameLoc.position;
    }
}
