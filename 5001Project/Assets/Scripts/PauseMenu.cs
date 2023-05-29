using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject Pause;
    [SerializeField] private List<GameObject> ElementsToPause;
    [SerializeField] private Transform outOfDisplay;
    public GameObject pieceList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PauseGame()
    {
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(false);
        }

        Pause.SetActive(true);

        pieceList.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(true);
        }
        pieceList.SetActive(true);


        Pause.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        for (int i = 0; i < pieceList.transform.childCount; i++)
        {
            pieceList.GetComponentInChildren<TimelinePiece>(i).releasePiece();
            pieceList.GetComponentInChildren<TimelinePiece>(i).transform.position = outOfDisplay.position;
        }
        PersistentData.Instance.GameUndone();
        PersistentData.Instance.ResetGame();
        pieceList.SetActive(true);
    }
}
