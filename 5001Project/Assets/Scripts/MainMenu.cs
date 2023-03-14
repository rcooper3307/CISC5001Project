using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text nameField;

    // Start is called before the first frame update
    void Start()
    {
        UpdateName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateName()
    {
        nameField.text = PersistentData.Instance.GetName();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void Library()
    {
        SceneManager.LoadScene("Library");
    }
}
