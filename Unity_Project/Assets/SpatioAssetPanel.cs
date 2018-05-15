using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpatioAssetPanel : MonoBehaviour {

    public Image parentImage;
	public Image image;
	// Use this for initialization
	void Start () {
	
	}

	public void SetPhoto(Sprite img){
		image.sprite = img;
	}
    public void SwitchTransparency()
    {
        Color c;
        c.r = c.g = c.b = 1.0f;
        c.a = .35f;
        if(image.color.a < 1f)
        {
            image.color = Color.white;
            parentImage.color = Color.white;
        }
        else
        {
            image.color = c;
            parentImage.color = c;
        }
        
    }
	public void Switch()
	{
        
		gameObject.SetActive (!gameObject.activeSelf);
        parentImage.color = Color.white;
        image.color = Color.white; 
	}

	public void CopyData()
	{
		string data = "";
		Transform title = transform.parent.Find ("Title");
		data = data + "Title: " + title.GetComponent<Text> ().text + "\r\n";
		Text[] txts = GetComponentsInChildren<Text> ();
		foreach(Text txt in txts)
		{
			if (txt.GetComponentInParent<Button> ())
				continue;
			data = data + txt.text + "\r\n";
		}
		TextEditor te = new TextEditor ();
		te.text = data;
		te.SelectAll ();
		te.Copy ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
