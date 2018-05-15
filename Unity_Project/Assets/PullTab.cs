using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class PullTab : MonoBehaviour {
	public bool deployed;
	// Use this for initialization
	void Start () {
	
	}
	public void Deploy()
	{
		RectTransform r = (RectTransform)transform;
		float offset = r.rect.width * (deployed ? -1 : 1) * (r.anchorMin.x == 0 ? 1 : -1);
		transform.position += new Vector3 (offset, 0, 0);
		deployed = !deployed;
		EventSystem.current.SetSelectedGameObject(null);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
