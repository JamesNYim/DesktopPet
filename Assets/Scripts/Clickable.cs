

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour
{
    private Camera mainCam;
    private WindowClickControl windowClickControl;
    private bool mouseOver = false;
    private Pet pet;

    void Start()
    {
        mainCam = Camera.main;
        windowClickControl = FindObjectOfType<WindowClickControl>();
        pet = GetComponent<Pet>();
    }

    void Update()
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool isOver = hit.collider != null && hit.collider.transform == transform;

        if (isOver && !mouseOver)
        {
            mouseOver = true;
            windowClickControl?.SetClickThrough(false); // make clickable
            Debug.Log("Mouse entered object!");
            pet.speak("Mouse Entered");
        }
        else if (!isOver && mouseOver)
        {
            mouseOver = false;
            windowClickControl?.SetClickThrough(true); // make click-through again
            Debug.Log("Mouse left Object!");
            pet.speak("Mouse left!");
        }

        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            if (pet != null)
            {
                pet.speak("Clicked!");
            }
            Debug.Log("Object clicked!");
        }
    }
}
