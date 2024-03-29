 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimelinePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private bool mouseDragging, placed;
    [SerializeField] private Vector2 offset, originalPos;
    [SerializeField] private int value;
    [SerializeField] private int seriesVal;
    [SerializeField] public TextMeshPro textChild;
    [SerializeField] private string description;
    [SerializeField] private string year;
    [SerializeField] private int order;
    [SerializeField] PersistentData p;
    public TimelineManager tManager;
    
    ProgressScript progress;


    private bool active = false;
    [SerializeField] private bool picked = false;
    private TimelineSlot Tslot;
    
    
    //When the object spawns, its original position is recorded
    void Awake()
    {
        originalPos = transform.position;
        if (textChild == null)
        {
            textChild = GetComponentInChildren<TextMeshPro>();
        }
        
    }

    void Start()
    {
        p = FindObjectOfType<PersistentData>();
        progress = FindObjectOfType<ProgressScript>();
       
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

        if(active == true)
        {
            //If the object is within dropping distance of the slot, then it stays in the slot spot.
            if (Vector2.Distance(transform.position, Tslot.transform.position) < 1)
            {
                p.Correct();
                
                transform.position = Tslot.transform.position;
               
                Tslot.Placed();
                placed = true;
            }
            //otherwise, it returns to its original position
            else
            {
                p.Wrong();
                StartCoroutine(tManager.WrongTextAppears());
                transform.position = originalPos;
                mouseDragging = false;
            }
        }
    }


    void Update()
    {
        if (tManager == null)
        {
            tManager = FindObjectOfType<TimelineManager>();
        }
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
    //Sets value of a piece
    public void setValue(int val)
    {
        value = val;
    }
    //Returns value of a piece
    public int getValue()
    {
        return value;
    }
    //Sets series of a piece
    public void setSeries(int val)
    {
        seriesVal = val;
    }
    //Returns series value of a piece
    public int getSeries()
    {
        return seriesVal;
    }

    public void setDesc(string desc)
    {
        description = desc;
    }

    public string getDesc()
    {
        return description;
    }

    public void setText(string text)
    {
        textChild.text = text;
    }
    //Change return position
    public void newPos()
    {
        originalPos = transform.position;
    }

    //Return Picked Status
    public bool isPicked()
    {
        return picked;
    }

    //Sets piece as picked
    public void pickPiece()
    {
        picked = true;
    }

    //Sets piece as not picked
    public void releasePiece()
    {
        picked = false;
    }

    public void setYear(string date)
    {
        year = date;
    }

    public string getYear()
    {
        return year;
    }

    public void setOrder(int pos)
    {
        order = pos;
    }

    public int getOrder()
    {
        return order;
    }
}
