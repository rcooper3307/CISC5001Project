using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    

    // Update is called once per frame
    void Update()
    {
     
    }
    public void LoadNextLevel(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        //play animation
        transition.SetTrigger("Start");
        //wait 1 second (length of animation)
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(scene);
    }
}
