using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimelineManager : MonoBehaviour
{

    [SerializeField] private List<TimelineSlot> slotPrefabs;
    [SerializeField] private TimelinePiece piecePrefab;
    [SerializeField] private Transform slotParent, pieceParent;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }
    //spawner
    void Spawn()
    {
        //Grabs random selection of pieces
        var randomSet = slotPrefabs.OrderBy(s => Random.value).Take(3).ToList();
        //spawns each slot and each piece
        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(randomSet[i], slotParent.GetChild(i).position, Quaternion.identity);
            var spawnedPiece = Instantiate(piecePrefab, pieceParent.GetChild(i).position, Quaternion.identity);
            spawnedPiece.Init(spawnedSlot);
        }
    }
}
  

