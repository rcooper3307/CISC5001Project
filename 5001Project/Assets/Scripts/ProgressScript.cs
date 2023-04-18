using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
    private Slider slider;
    public GameObject pieceList;

    public float fillSpeed = 0.5f;
    float increment;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = PersistentData.Instance.progressBar;
        if (pieceList == null)
        {
            pieceList = GameObject.FindGameObjectWithTag("pieceList");
        }
        //calculates how much the bar should move after a correct answer is scored
        //((1/(number of pieces in piecelist)) * (number of pieces in a series)) / (number of rounds seen with that series)
        increment = (float)(((float)1/(pieceList.transform.childCount) * 3)/2);
    }

    // this is what animates the progress bar.
    void Update()
    {
        if (slider.value < PersistentData.Instance.progressBar)
            slider.value += fillSpeed * Time.deltaTime;

    }
    //this allows the progress bar's value to be changed by a custom float value
    public void AddProgress(float progress)
    {
        PersistentData.Instance.progressBar = slider.value + progress;
    }
    //by default, the progress bar is incremented by the value stored in increment
    public void AddProgress()
    {
        AddProgress(increment);
        Debug.Log("increment equals " + increment);
    }
}
