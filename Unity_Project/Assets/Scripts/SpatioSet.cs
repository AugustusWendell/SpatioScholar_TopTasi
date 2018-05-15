using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpatioSet : MonoBehaviour {
	public SpatioModel[] list;
	public SpatioModel Current;
    public SpatioManager manager;
    public string Description;
    public Slider slider;
    public Text TestText;
    public Text InfoText;
    public Text DateText;
    public Image timelockIcon;
    public Sprite locked;
    public Sprite unlocked;
    public Date start;
    public Date end;
	SpatioModel Previous;
    private int Year;
	float TransitionTimer;
	public float MaxTransitionTimer = 2.0f;
	// Use this for initialization
	void Start ()
	{
	    bool minSet = false, maxSet = false;

		list = GetComponentsInChildren<SpatioModel> (true);
		Current = list [0];
	    
		Current.opacity = 1.0f;

        
	    foreach (SpatioModel ro in list)
	    {
            if (!maxSet || ro.start.Year > (int)slider.maxValue)
            {
                maxSet = true;
                //Avoids the risk of weird slider crash
                if (ro.start.Year < (int)slider.minValue)
                {
                    minSet = true;
                    slider.minValue = ro.start.Year;
                    start = ro.start;
                }
                slider.maxValue = ro.start.Year;
                end = ro.end;
            }
            if (!minSet || ro.start.Year < (int)slider.minValue)
            {
	            minSet = true;
                start = ro.start;
                slider.minValue = ro.start.Year;
            }
	    }
	    Year = Current.start.Year;
	    SetDateText();
	    SetInfoText();
        slider.onValueChanged.AddListener(delegate { YearChangeCheck(); });
	}

    void YearChangeCheck()
    {
        if ((int)slider.value != Year)
        {
            SwitchTo((int)slider.value);
            if (Year != manager.date.Year)
            {
                manager.setVaries(true);
            }
        }

    }

    public void ToggleSlider()
    {
        slider.interactable = !slider.interactable;
        if (slider.interactable)
        {
            timelockIcon.sprite = unlocked;
        }
        else
        {
            timelockIcon.sprite = locked;    
        }
        
        
    }

	public void SwitchVisibleModel(string tag, bool visible)
	{
		foreach (SpatioModel r in list) {
			if (r.tags != null && r.tags.Contains (tag)
                && r.start.Year <= Year && r.end.Year >= Year)
            {
				r.gameObject.SetActive (visible);
			    r.hidden = !visible;
			}
		}
	}
    public void SetDateText()
    {
        //DateText.text = "Circa " + Year + (Current != null ? "\n(Est. " + Current.start.Year + ")" : "");
    }

    public void SetInfoText()
    {
       // InfoText.text = Description + "\n" + Current.Description;
    }
	public void SwitchTo(int year)
	{
	    Year = year;
		SpatioModel closest = null;
		foreach (SpatioModel r in list) 
		{
			if (!r.hidden && r.start.Year <= year && r.end.Year >= year)
				r.gameObject.SetActive (true);
			else
				r.gameObject.SetActive (false);
            /*if (r.start.Year <= year && (closest == null || r.start.Year - closest.start.Year >= 0))
			{
				closest = r;
			}*/
		}
        
        /*slider.value = year;
		if (closest != Current)
		{
			Previous = Current;
            Current = closest;
            
			TransitionTimer = MaxTransitionTimer;
			if(Previous)
				Previous.gameObject.SetActive(true);
			if(Current)
				Current.gameObject.SetActive(true);
		    
            SetInfoText();
		}
        SetDateText();
        */
	}
	// Update is called once per frame
	void Update () {
		if (TransitionTimer > 0.0f) {
			TransitionTimer -= Time.deltaTime;
			if(Current != null)
				Current.opacity = 1.0f - TransitionTimer / MaxTransitionTimer;
			if(Previous != null)
				Previous.opacity = TransitionTimer / MaxTransitionTimer;
		} else {
			Previous = null;
			/*foreach(SpatioModel r in list){
				if(r != Current)
					r.gameObject.SetActive(false);
				else
					r.gameObject.SetActive(true);
			}*/
		}
	}
}
