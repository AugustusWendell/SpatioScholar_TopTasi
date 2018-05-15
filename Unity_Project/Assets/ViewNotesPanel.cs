using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SimpleJSON;

public class ViewNotesPanel : MonoBehaviour
{
    public GameObject canvas;
    public NotesPanel view;
    GameObject blurbResource;

    // Use this for initialization
    void Start()
    {
        blurbResource = Resources.Load<GameObject>("Note Blurb");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateNotes(JSONNode node, DateTime lastUpdate)
    {
        int children = canvas.transform.childCount;
        foreach (JSONNode n in node.AsArray)
        {
            DateTime postTime = DateTime.ParseExact(n["posttime"], "yyyy-MM-dd HH:mm:ss", null);
            Debug.Log("postTime: " + postTime + " lastUpdate: " + lastUpdate);
            if (postTime <= lastUpdate)
                continue;
            GameObject blurb = Instantiate<GameObject>(blurbResource);
            NoteBlurb b = blurb.GetComponent<NoteBlurb>();
            b.Setup(view, n["first"], n["last"], n["brief"],
                    n["full"], n["url"],
                    new Vector3(n["x"], n["y"], n["z"]),
                    postTime,
                    DateTime.ParseExact(n["reftime"], "yyyy-MM-dd HH:mm:ss", null));
            blurb.transform.SetParent(canvas.transform);
            RectTransform rt = blurb.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(5, -5 - rt.sizeDelta.y * children, 0);
            children++;
        }
    }
}

