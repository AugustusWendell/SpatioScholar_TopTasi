using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundRetriever : MonoBehaviour {
	public AudioPlayer player;
	public AudioClip clip;
	public string url;
	// Use this for initialization
	void Start () {
		clip = null;
		StartCoroutine (DownloadSound ());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator DownloadSound()
	{
		if (clip != null)
			yield break;
		WWW www = new WWW (url);
		yield return www;
		clip = www.GetAudioClip();
		player.source.clip = clip;
	}
}
