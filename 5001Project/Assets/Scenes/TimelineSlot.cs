using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSlot : MonoBehaviour
{
    public SpriteRenderer renderer;
    TimelineManager manager;

    void Awake()
    {
        manager = FindObjectOfType<TimelineManager>();
    }

    public void Placed()
    {
        manager.Restart();
    }
}

