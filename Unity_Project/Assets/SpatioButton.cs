using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpatioButton : MonoBehaviour {
	public GameObject asset;
    public Image image;
	// Use this for initialization
	void Start () {
	}
	public void OnClick()
	{
		asset.SetActive (!asset.activeSelf);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
