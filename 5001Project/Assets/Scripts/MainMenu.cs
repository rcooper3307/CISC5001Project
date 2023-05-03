using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text nameField;
    [SerializeField] public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        UpdateName();
        if (levelLoader == null)
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMainMenu()
    {
        levelLoader.LoadNextLevel("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }

    void UpdateName()
    {
        nameField.text = PersistentData.Instance.GetName();
    }

    public void PlayGame()
    {
        levelLoader.LoadNextLevel("PickOne");
        //SceneManager.LoadScene("PickOne");
    }

    public void Instructions()
    {
        levelLoader.LoadNextLevel("Instructions");
        //SceneManager.LoadScene("Instructions");
    }

    public void Settings()
    {
        levelLoader.LoadNextLevel("Settings");
        //SceneManager.LoadScene("Settings");
    }
    
    public void Library()
    {
        levelLoader.LoadNextLevel("Library");
        //SceneManager.LoadScene("Library");
    }

    public void Return()
    {
        GameObject pieceList;
        pieceList = GameObject.FindGameObjectWithTag("pieceList");
        while (pieceList.transform.childCount > 0)
        {
            DestroyImmediate(pieceList.transform.GetChild(0).gameObject);
        }
        levelLoader.LoadNextLevel("LevelSelect");
    }
}
