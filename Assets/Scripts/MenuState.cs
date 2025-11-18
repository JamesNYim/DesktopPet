using UnityEngine;
using System.Collections;

public class MenuState : IPetState
{
    private Pet pet;
    private PetStateMachine fsm;
    private GameObject activeMenu;
    private MenuUI menuUI;

    public MenuState(Pet pet, PetStateMachine fsm)
    {
        this.pet = pet;
        this.fsm = fsm;
    }

    public void Enter()
    {
        // Spawn menu
        pet.speak("Menu");
        pet.animate("Play");
        // Spawn menu 
        activeMenu = GameObject.Instantiate(pet.menuPrefab, pet.menuCanvas.transform);
        menuUI = activeMenu.GetComponent<MenuUI>();

        RectTransform rt = activeMenu.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);

        // get pet bottom in screen space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pet.transform.position);
        float petScreenHeight = Camera.main.WorldToScreenPoint(pet.transform.position + Vector3.up * (pet.spriteRenderer.bounds.size.y / 2f)).y - screenPos.y;

        // position menu so it aligns
        Vector3 menuPos = screenPos + new Vector3(0f, -petScreenHeight, 0f);
        rt.position = menuPos;
        
        // Clamp to screen edges
        ClampMenuToScreen(rt);

        // Exit menu behavior
        pet.StartCoroutine(CloseMenuClickOutside());
    }

    public void Update()
    {
        // Optional: hover effects, animations
        pet.stats.Idle(Time.deltaTime);
        menuUI.UpdateStats(pet.stats);
    }

    public void Exit()
    {
        pet.speak("Goodbye Menu");
        if (activeMenu != null)
            GameObject.Destroy(activeMenu);
    }

    public void PositionPetOnMenu(GameObject menu, Transform petTransform)
    {
        RectTransform rt = menu.GetComponent<RectTransform>();
        Vector3 screenPos = rt.position; // menu position in screen space

        // Convert screen space to world space (camera dependent)
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = petTransform.position.z; // maintain pet's Z if using 2D

        // Offset slightly above menu
        Vector3 offset = new Vector3(0f, rt.sizeDelta.y / 100f, 0f); // adjust scale as needed
        petTransform.position = worldPos + offset;
    }

    private void ClampMenuToScreen(RectTransform rt)
    {
        Vector3 pos = rt.position;
        Vector2 size = rt.sizeDelta;
        float halfWidth = size.x * 0.5f;
        float height = size.y;
        pos.x = Mathf.Clamp(pos.x, halfWidth, Screen.width - halfWidth);
        pos.y = Mathf.Clamp(pos.y, height, Screen.height);
        rt.position = pos;
    }

    private IEnumerator CloseMenuClickOutside()
    {
        yield return null;
        while (activeMenu != null)
        {
            if (Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(
                activeMenu.GetComponent<RectTransform>(), Input.mousePosition))
            {
                fsm.RequestState(PetStateMachine.PetState.Idle); // leave MenuState
                pet.speak("CloseMenuClickOutside()");
                yield break;
            }
            yield return null;
        }
    }
}
