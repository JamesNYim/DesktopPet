using UnityEngine;

public class WalkState : IPetState
{
    private Pet pet;
    private PetStateMachine fsm;
    private float timer;
    private Vector3 targetPos;

    public WalkState(Pet pet, PetStateMachine fsm)
    {
        this.pet = pet;
        this.fsm = fsm;
    }

    public void Enter()
    {
        pet.speak("Walking");
        pet.animate("Walk");
        timer = 0f;
        // Pick a random destination within bounds
        targetPos = pet.GetRandomWalkPoint();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        // Move toward target
        Vector3 dir = (targetPos - pet.petTransform.position).normalized;
        pet.petTransform.position += dir * pet.speed * Time.deltaTime;
        pet.energy -= Time.deltaTime * 5f;

        // Flip sprite
        pet.spriteRenderer.flipX = dir.x < 0;

        // Check if reached destination
        if (Vector3.Distance(pet.petTransform.position, targetPos) < 0.1f) {
            fsm.RequestState(PetStateMachine.PetState.Idle);
        }

        // Sleeping if tired
        if (pet.energy < 20) {
            fsm.RequestState(PetStateMachine.PetState.Sleep);
        }

        // Happiness go down
        pet.happiness -= Time.deltaTime;
    }

    public void Exit() { }
}
