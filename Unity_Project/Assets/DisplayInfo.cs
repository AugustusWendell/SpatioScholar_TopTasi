using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{
    public GameObject info;
    public bool infoShown;
	// Use this for initialization
	void Start ()
	{
	    infoShown = false; 
	}

    public void toggleInfo()
    {
        if (infoShown)
        {
            infoShown = false;
        }
        else
        {
            infoShown = true;
        }
		info.SetActive(infoShown);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
