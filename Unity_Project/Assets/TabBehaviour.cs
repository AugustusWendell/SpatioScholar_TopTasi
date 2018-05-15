using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Tab Navigator for UI
// Single instance of this script per GUI
// An alternative would be to use a next/previous setting on a single GUI item, which would mean one script per InputField - not ideal

public class TabBehaviour : MonoBehaviour
{
    private EventSystem system;

    private void Start()
    {
        system = EventSystem.current;
    }

    private void Update()
    {
        if (system.currentSelectedGameObject == null || !Input.GetKeyDown(KeyCode.Tab))
            return;
        bool up = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        Selectable current = system.currentSelectedGameObject.GetComponent<Selectable>();
        if (current == null)
            return;
        int i = -1;
        Selectable[] list = null;
        if(current.transform.parent)
        {
            list = current.transform.parent.GetComponentsInChildren<Selectable>();
            for(i = 0;i < list.Length;i++)
            {
                if(list[i].gameObject == current.gameObject)
                {
                    break;
                }
            }
        }
        if (i == -1)
        {

            Selectable next = up ? current.FindSelectableOnUp() : current.FindSelectableOnDown();

            // We are at the end or the beginning, go to either, depends on the direction we are tabbing in
            // The previous version would take the logical 0 selector, which would be the highest up in your editor hierarchy
            // But not certainly the first item on your GUI, or last for that matter
            // This code tabs in the correct visual order
            if (next == null)
            {
                next = current;

                Selectable pnext;
                if (up) while ((pnext = next.FindSelectableOnDown()) != null) next = pnext;
                else while ((pnext = next.FindSelectableOnUp()) != null) next = pnext;
            }
            // Simulate Inputfield MouseClick
            InputField inputfield = next.GetComponent<InputField>();
            if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));
            system.SetSelectedGameObject(next.gameObject);
        }
        else if(list != null)
        {
            if(up)
            {
                
                if (i == 0) 
                    i = list.Length - 1;
                else
                    i--;
                Selectable s = list[i];
                if (s is InputField) ((InputField)s).OnPointerClick(new PointerEventData(system));
                system.SetSelectedGameObject(s.gameObject);
            }
            else
            {
                if (i == list.Length-1)
                    i = 0;
                else
                    i++;
                Selectable s = list[i];
                if (s is InputField) ((InputField)s).OnPointerClick(new PointerEventData(system));
                system.SetSelectedGameObject(s.gameObject);
            }
        }
        // Select the next item in the taborder of our direction
        
    }
}

