using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {
	public GameObject overheadObject;
	public GameObject FPSObject;
	public SpatioManager manager;
	public bool overhead;
	// Use this for initialization
	void Start () {
		overheadObject.SetActive (true);
		FPSObject.SetActive (false);
        //DelayedSwitch(1);
    }

    IEnumerator DelayedSwitch(float time)
    {
        yield return new WaitForSeconds(time);
        SwitchView();
    }

	public void SwitchView()
	{
        Debug.Log("Test");
		overhead = !overhead;
		overheadObject.SetActive (overhead);

		FPSObject.SetActive (!overhead);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        
		//manager.SwitchVisibleModel ("ROOF", !overhead);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
