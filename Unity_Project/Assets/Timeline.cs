using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public Slider slider;
    public Text timelineText;
    public bool localTimeline;
    public SpatioSet set;
	// Use this for initialization
	void Start ()
	{
	    localTimeline = false;
	    timelineText.text = "Global";
	}
    //Switch the timeline
    public void SwitchTimeline()
    {
        if (localTimeline)
        {
            localTimeline = false;
            timelineText.text = "Global";
            slider.minValue = set.manager.start.Year;
            slider.maxValue = set.manager.end.Year;
        }
        else
        {
            localTimeline = true;
            timelineText.text = "Local";
            slider.minValue = set.start.Year;
            slider.maxValue = set.end.Year;
        }
    }
	// Update is called once per frame
	void Update () {
	    
	}
}
