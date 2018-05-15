using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
	public AudioSource source;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayClip()
	{
		if (source.clip == null)
			return;
		source.Play ();
	}
}
