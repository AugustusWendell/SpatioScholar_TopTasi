using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimePlaque : MonoBehaviour
{
    public Slider timeSlider;
    public Text time;
	// Use this for initialization
    private void Start()
    {
        timeSlider.onValueChanged.AddListener(delegate { updateTime(); });
    }

    public void updateTime()
    {
        time.text = "" + timeSlider.value;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
