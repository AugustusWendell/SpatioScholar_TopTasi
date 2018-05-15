using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpToPoint : MonoBehaviour {
    public GameObject fpsController;
    public ViewController viewController;
    public Vector3 lastClick;
    public Text coordText;
    new Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (viewController.overhead)
        {
            if (Input.GetMouseButtonDown(2))
            {

                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                foreach (RaycastHit hit in Physics.RaycastAll(ray))
                {
                    if (hit.transform.tag != "SelectableRegion")
                        continue;
                    SelectableRegion region = hit.transform.GetComponent<SelectableRegion>();
                    JumpTo(region);
                    // Do something with the object that was hit by the raycast.
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                float closestDistance = 1000;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                float range = camera.farClipPlane - camera.nearClipPlane;
                foreach (RaycastHit hit in Physics.RaycastAll(ray))
                {
                    if (hit.distance < range && hit.distance < closestDistance)
                    {
                        coordText.text = "Coordinates: " + hit.point;
                        lastClick = hit.point;
                        closestDistance = hit.distance;
                    }

                }
            }
        }
	}

    void JumpTo(SelectableRegion region)
    {
        fpsController.transform.position = region.jumpPoint;
        viewController.SwitchView();
    }
}
