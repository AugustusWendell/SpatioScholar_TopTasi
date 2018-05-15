using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpatioManager : MonoBehaviour 
{
	SpatioSet[] sets;
    public Date date;
    public Date start;
    public Date end;
    private Slider slider;
    bool varies;
	bool changed;
    private int floor;
    public GameObject fps;
    public Text coordText;
    public Text rotText;
    DBLoader database;
	// Use this for initialization
	void Start () 
	{
		sets = FindObjectsOfType<SpatioSet> ();
	    slider = GetComponentInChildren<Slider>();
        database = gameObject.GetComponent<DBLoader>();
	    slider.minValue = start.Year;
	    slider.maxValue = end.Year;
	    foreach (SpatioSet rs in sets)
	    {
	        rs.manager = this;
	    }
	    varies = false;
	    date = start;
        changed = true;
	}

    public void setVaries(bool doesVary)
    {
        varies = doesVary;
    }

    public void SwitchToFloor2()
    {
        floor = 2;
        SwitchVisibleModel("FLOOR2", true);
        
    }
    public void SwitchToFloor1()
    {
        fps.transform.position = new Vector3(58.2f, 2.8f, -47.3f);
        fps.transform.rotation = new Quaternion(0.0f, 108.4f, 0.0f, 1.0f);
        floor = 1;
        SwitchVisibleModel("FLOOR2", false);

    }
	public void SwitchVisibleModel(string tag, bool visible)
	{
		foreach (SpatioSet rs in sets) {
			rs.SwitchVisibleModel (tag, visible);
		}
	}


    public void AddNote(string first, string last, string brief, string full, string URL, System.DateTime date, Vector3 location)
    {
        StartCoroutine(database.AddNote(first, last, brief, full, URL, date, location));
    }
    // Update is called once per frame
    void Update () 
	{
		if (changed /*&& !varies*/)
		{
		    slider.value = date.Year;
			foreach (SpatioSet rs in sets) 
			{
				rs.SwitchTo (date.Year);
			}
			changed = false;
		}

	    if ((int)slider.value != date.Year)
	    {
	        date.Year = (int)slider.value;
	        changed = true;
	    }

		if(Input.GetKeyDown (KeyCode.L))
		{
            coordText.text = "Coordinates: " + fps.transform.position;
            rotText.text = "Rotation: " + fps.transform.rotation.eulerAngles;
		}else if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
	}
}
