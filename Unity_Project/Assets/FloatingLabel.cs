using UnityEngine;
using System.Collections;


public class FloatingLabel : MonoBehaviour
{
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Camera camera = Camera.main;
	    if (camera == null)
	        return;
        Vector3 toTarget = (camera.transform.position - transform.position);
        if(toTarget.magnitude > 20)
            foreach( Renderer r in gameObject.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
        else
            foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
            {
                r.enabled = true;
            }
        if (Vector3.Dot(toTarget.normalized, transform.forward) > 0) {
            transform.Rotate(new Vector3(0, 180));
        }
	}
}
