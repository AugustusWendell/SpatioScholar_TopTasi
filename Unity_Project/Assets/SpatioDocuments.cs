using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpatioDocuments : MonoBehaviour {
    List<SpatioButton> buttons;
    bool changed;
	// Use this for initialization
	void Start () {
        if (buttons == null)
            buttons = new List<SpatioButton>();
	}
	
    void UpdateButtons()
    {
        int count = 0;
        foreach (SpatioButton b in buttons)
        {
            if (b.gameObject.active == false) { 
                count++;
                continue;
            }
            RectTransform r = b.GetComponent<RectTransform>();
            if (count % 2 == 0)
            {
                r.offsetMin = new Vector2(-90, -125 - count / 2 * 75);
                r.offsetMax = new Vector2(0, -50 - count / 2 * 75);
            }
            else
            {
                r.offsetMin = new Vector2(0, -125 - count / 2 * 75);
                r.offsetMax = new Vector2(90, -50 - count / 2 * 75);
            }
            count++;
        }
    }

    public void AddButton(SpatioButton b)
    {
        if (buttons == null)
            buttons = new List<SpatioButton>();
        buttons.Add(b);
        changed = true;
    }

	// Update is called once per frame
	void Update () {
        if (changed)
        {
            UpdateButtons();
            changed = false;
        }
	}
}
