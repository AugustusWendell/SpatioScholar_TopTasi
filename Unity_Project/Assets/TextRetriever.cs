using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextRetriever : MonoBehaviour {
	public string url;
	public Text GUItext;
	// Use this for initialization
	void Start () {
		StartCoroutine (DownloadText ());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator DownloadText()
	{
		WWW www = new WWW (url);
		yield return www;
		GUItext.text = www.text;
	}
}
