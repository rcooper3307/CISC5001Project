using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] int seriesOn;
    [SerializeField] public int[] valueOfPieces = new int[2];
    [SerializeField] bool done = false;

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
}
