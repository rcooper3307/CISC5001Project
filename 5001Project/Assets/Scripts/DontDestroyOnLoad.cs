using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool GameManagerExists;
    // Use this for initialization
    void Start ()
    {
        if (!GameManagerExists) //if GameManagerexcistst is not true --> this action will happen.
        {
            GameManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    // Update is called once per frame
    void Update () {
       
    }
}
