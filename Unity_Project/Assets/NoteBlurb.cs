using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NoteBlurb : MonoBehaviour {
    public NotesPanel noteView;
    public Text nameText;
    public Text shortText;
    public Text postDateText;
    public Text locationText;
    public Text refDateText;
    string m_first;
    string m_last;
    string m_brief;
    string m_full;
    string m_url;
    Vector3 m_location;
    DateTime m_posttime;
    DateTime m_reftime;
	// Use this for initialization
	void Start () {
	}
	
    public void Setup(NotesPanel view, string first, string last, string brief, 
               string full, string url, Vector3 loc, 
               System.DateTime post, System.DateTime refDate)
    {
        noteView = view;
        //Update variables
        m_first = first;
        m_last = last;
        m_brief = brief;
        m_full = full;
        m_url = url;
        m_location = loc;
        m_posttime = post;
        m_reftime = refDate;
        //Fill text fields
        nameText.text = last + ", " + first;
        shortText.text = brief;
        locationText.text = loc.ToString();
        postDateText.text = m_posttime.ToString("dd/MM/yyyy");
        refDateText.text = m_reftime.ToString("dd/MM/yyyy");
    }

    public void View()
    {
        noteView.gameObject.SetActive(true);
        noteView.Set(m_first, m_last, m_brief, m_full, m_url, m_location,m_posttime,m_reftime);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
