using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Date
{
    public int Year;
    public int Month;
    public int Day;
}

public class SpatioModel : MonoBehaviour {
    


    public Date start;
    public Date end;
    public Vector3 Anchor;
    public string Description;
	new MeshRenderer renderer;
	public float opacity;
	public List<string> tags;
    public string block;
    public bool hidden;
	// Use this for initialization
	void Start ()
	{
	    hidden = false;
		renderer = GetComponent<MeshRenderer> ();
	}
	// Update is called once per frame
	void Update () {
		if (renderer == null)
			return;
		Material m = renderer.materials[0];
		m.color = new Color(m.color.r, m.color.g, m.color.b, opacity);
	}
}
