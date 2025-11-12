
using UnityEngine;

public class PetInteractionHandler: MouseInteractable
{
    private Pet pet;

    protected override void Start()
    {
        base.Start();
        pet = GetComponent<Pet>();
    }

    protected override void OnMouseClick()
    {
        pet.speak("Clicked");
    }

    protected override void OnMouseEnterObject()
    {
        pet.speak("Mouse entered pet!");
    }

    protected override void OnMouseExitObject()
    {
        pet.speak("Mouse left pet!");
    }

    protected override void OnMouseDrag()
    {
        // Optional: add animation or sound while dragging
        pet.speak("Im being kidnapped!");
    }
}
