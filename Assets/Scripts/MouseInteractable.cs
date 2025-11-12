
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MouseInteractable : MonoBehaviour
{
    protected Camera mainCam;
    protected WindowClickControl windowClickControl;
    protected bool mouseOver = false;
    protected bool isDragging = false;

    private Vector3 dragOffset;
    private float dragZ;

    protected virtual void Start()
    {
        mainCam = Camera.main;
        windowClickControl = FindObjectOfType<WindowClickControl>();
    }

    protected virtual void Update()
    {
        HandleHover();
        HandleClick();
        HandleDrag();
    }

    private void HandleHover()
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool isOver = hit.collider != null && hit.collider.transform == transform;

        if (isOver && !mouseOver)
        {
            mouseOver = true;
            windowClickControl?.SetClickThrough(false);
            OnMouseEnterObject();
        }
        else if (!isOver && mouseOver && !isDragging)
        {
            mouseOver = false;
            windowClickControl?.SetClickThrough(true);
            OnMouseExitObject();
        }
    }

    private void HandleClick()
    {
        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            dragZ = mainCam.WorldToScreenPoint(transform.position).z;
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragZ);
            dragOffset = transform.position - mainCam.ScreenToWorldPoint(mouseScreenPos);

            isDragging = true;
            OnMouseClick();
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            mouseOver = false;
            windowClickControl?.SetClickThrough(true);
            OnMouseRelease();
        }
    }

    private void HandleDrag()
    {
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragZ);
            Vector3 targetPos = mainCam.ScreenToWorldPoint(mouseScreenPos) + dragOffset;
            transform.position = targetPos;
            OnMouseDrag();
        }
    }

    // 🔹 Hooks for subclasses or other scripts
    protected virtual void OnMouseEnterObject() { }
    protected virtual void OnMouseExitObject() { }
    protected virtual void OnMouseClick() { }
    protected virtual void OnMouseDrag() { }
    protected virtual void OnMouseRelease() { }
}
