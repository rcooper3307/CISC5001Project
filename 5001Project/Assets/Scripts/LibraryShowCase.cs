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
    public Text year;
    public Text description;
    public string titlestring;
    public bool specialPage = false;

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
            currentpiece++;
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring;
            year.text = "" + pieces[currentpiece].getYear();
            description.text = ""+ pieces[currentpiece].getDesc();
        }
        else if(PersistentData.Instance.CheckCompletion())
        {
            specialPage = true;
            titlestring = PersistentData.Instance.GetName();
            titleField.text = titlestring;
            year.text = PersistentData.Instance.GetDate() +" CE";
            description.text = "The year where you completed the game!";
        }
        else
        {
            Debug.Log("There is nothing on the right of this piece!");
        }
    }

    public void GoLeft()
    {
        if (specialPage)
        {
            specialPage = false;
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring;
            year.text = "" + pieces[currentpiece].getYear();
            description.text = ""+ pieces[currentpiece].getDesc();
        }
        else if(currentpiece != 0)
        {
            currentpiece--;
            titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
            titleField.text = titlestring;
            year.text = "" + pieces[currentpiece].getYear();
            description.text = ""+ pieces[currentpiece].getDesc();
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
            for (int j = 0; j < piecelistsize; j++)
            {
                placeholder = pieceList.GetComponentInChildren<TimelinePiece>(j);
                if (placeholder.getOrder() == i+1)
                {
                    pieces.Add(placeholder);
                    break;
                }
            }
        }

        piecelistsize = pieces.Count;

        titlestring = pieces[currentpiece].GetComponentInChildren<TextMeshPro>().text;
        titleField.text = titlestring;
        year.text = "" + pieces[currentpiece].getYear();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
