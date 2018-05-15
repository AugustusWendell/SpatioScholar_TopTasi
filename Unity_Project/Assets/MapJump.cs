using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MapJump : MonoBehaviour, IPointerClickHandler
{
    public Image map;
    public Texture2D color_map;
    public Material mat;
	// Use this for initialization
	void Start () {
        mat.SetColor("_KeyColor", new Color(0, 0, 0, 0));
	}
    
    //Override 
    public void OnPointerClick(PointerEventData ED)
    {
        Vector3 localHit = transform.InverseTransformPoint(ED.pressPosition);
        Vector3 dest = localHit;
        //Hack-y but workable for now
        dest.x = (dest.x + 50) / 100 * 1692;
        dest.y = (dest.y + 50) / 100 * 1360;
        
        Color c = color_map.GetPixel((int)dest.x, (int)dest.y);
       // Debug.Log("Collision Entered at: " + localHit);
        //Debug.Log("Converted to: " + dest);
        //Debug.Log("Color is: " + c);
        mat.SetColor("_KeyColor", c);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
