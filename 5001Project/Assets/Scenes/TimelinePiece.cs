 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private bool mouseDragging, placed;
    [SerializeField] private Vector2 offset, originalPos;
    [SerializeField] private int value;
    private bool active = false;
    private TimelineSlot Tslot;
    
    //When the object spawns, its original position is recorded
    void Awake()
    {
        originalPos = transform.position;
    }
    
    //When the mouse is held down, the object is picked up and grabbed
    void OnMouseDown()
    {
        if(active)
        {
            mouseDragging = true;

            //This exists so that the object doesn't immediately snap to the mouse position when it is grabbed.
            offset = GetMousePos() - (Vector2)transform.position;
        }
    }

    //When the mouse is let go and the object is not in a slot on the timeline, it goes back to its original position
    void OnMouseUp()
    {
        //If the object is within dropping distance of the slot, then it stays in the slot spot.
        if(Vector2.Distance(transform.position,Tslot.transform.position) < 3)
        {
            transform.position = Tslot.transform.position;
            Tslot.Placed();
            placed = true;
        }
        //otherwise, it returns to its original position
        else
        {
            transform.position = originalPos;
            mouseDragging = false;
        }
    }

    void Update()
    {
        //If the piece has been placed, don't do anything else
        if (placed) return;
        //If the piece isn't being picked up, leave it where it is
        if (!mouseDragging) return;
        //Moves the position of the object to the mouse position if it is being held down
        var mousePosition = GetMousePos();

        transform.position = mousePosition - offset;      
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Init(TimelineSlot slot)
    {
        //renderer.sprite = slot.renderer.sprite;
        Tslot = slot;
    }

    //Makes piece draggable
    public void activate()
    {
        active = true;
    }

    //Makes piece undraggable
    public void deactivate()
    {
        active = false;
    }

    //Returns value of a piece
    public int getValue()
    {
        return value;
    }

    //Change return position
    public void newPos()
    {
        originalPos = transform.position;
    }
}
