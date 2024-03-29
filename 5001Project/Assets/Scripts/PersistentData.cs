using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] string date;
    [SerializeField] int seriesOn;
    [SerializeField] public int[] valueOfPieces = new int[2];
    [SerializeField] bool done = false;
    [SerializeField] bool completed = false;
    [SerializeField] AudioSource rightAnswer;
    [SerializeField] AudioSource wrongAnswer;
    public int finalSeries = 999;
    public float progressBar = 0;


    public static PersistentData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = "";
    }

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.GetActiveScene().name == "Menu" || "PickOne")

    }

    public void SetName(string s)
    {
        playerName = s;
    }

    public string GetName()
    {
        return playerName;
    }

    public void GameDone()
    {
        done = true;
    }

    public void GameUndone()
    {
        done = false;
    }

    public bool GameStatus()
    {
        return done;
    }

    public int GetSeries()
    {
        return seriesOn;
    }

    public void SetSeries(int SV)
    {
        seriesOn = SV;
    }

    public int GetPieceOne()
    {
        return valueOfPieces[0];
    }

    public int GetPieceTwo()
    {
        return valueOfPieces[1];
    }

    public void SetPieces(int POne, int PTwo)
    {
        valueOfPieces[0] = POne;
        valueOfPieces[1] = PTwo;
    }

    IEnumerator Delay()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for .1 seconds.
        yield return new WaitForSeconds(0.1f);
        AudioSource.PlayClipAtPoint(rightAnswer.clip, new Vector2(0, 0));

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        // Code to execute after 3 second delay
    }

    public void Correct()
    {
        Debug.Log("COOOOOOOOORRRRRRRRRRRRECT");
        StartCoroutine(Delay());
         
    }

    public void Wrong()
    {
        Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE WRONG");
        
        AudioSource.PlayClipAtPoint(wrongAnswer.clip, new Vector2(0, 0));
    }

    public void ResetGame()
    {
        progressBar = 0;
    }

    public bool CheckCompletion()
    {
        return completed;
    }

    public void SetCompletion(bool status)
    {
        completed = status;
    }

    public void SetDate(string d)
    {
        date = d;
    }

    public string GetDate()
    {
        return date;
    }
}
