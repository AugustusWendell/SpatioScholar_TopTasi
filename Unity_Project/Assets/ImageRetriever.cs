using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ImageRetriever : MonoBehaviour {
	public string url;
	public Image image;
	Texture2D texture;
	// Use this for initialization
	void Start () {
		texture = null;
		StartCoroutine (DownloadImage ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DownloadImage()
	{
		if (texture != null)
			yield break;
		texture = new Texture2D (1, 1);
		WWW www = new WWW (url);
		yield return www;
		www.LoadImageIntoTexture (texture);
		image.sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
	}
}
