using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject Pause;
    [SerializeField] private List<GameObject> ElementsToPause;
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

        Time.timeScale = 0f;

        Pause.SetActive(true);

        pieceList.SetActive(false);
    }

    public void Resume()
    {
        for (int i = 0; i < ElementsToPause.Count; i++)
        {
            ElementsToPause[i].SetActive(true);
        }
        pieceList.SetActive(true);

        Time.timeScale = 1f;

        Pause.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        for (int i = 0; i < pieceList.transform.childCount; i++)
            pieceList.GetComponentInChildren<TimelinePiece>(i).releasePiece();
        PersistentData.Instance.GameUndone();
        PersistentData.Instance.ResetGame();
        pieceList.SetActive(true);
    }
}
