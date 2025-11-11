using UnityEngine;

public class IdleState : IPetState
{
    private Pet pet;
    private PetStateMachine fsm;
    private float timer;

    public IdleState(Pet pet, PetStateMachine fsm)
    {
        this.pet = pet;
        this.fsm = fsm;
    }

    public void Enter()
    {
        pet.speak("Idling...");
        pet.animator.Play("Idle");
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        pet.happiness -= Time.deltaTime;

        // Sleeping
        if (pet.energy < 20)
            fsm.ChangeState(PetStateMachine.PetState.Sleep);
        
        // Playing
        else if (pet.happiness < 20) {
            fsm.ChangeState(PetStateMachine.PetState.Play);
        }
        
        // Walking
        else if (timer > Random.Range(5f, 10f)) {
            fsm.ChangeState(PetStateMachine.PetState.Walk);
        }
    }

    public void Exit() { }
}
