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
        pet.speak("Idling");
        pet.animate("Idle");
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        pet.stats.Idle(Time.deltaTime);

        // Sleeping
        if (pet.stats.energy < 20)
            fsm.RequestState(PetStateMachine.PetState.Sleep);
        
        // Playing
        else if (pet.stats.happiness < 20) {
            fsm.RequestState(PetStateMachine.PetState.Play);
        }
        
        // Walking
        else if (timer > Random.Range(1f, 5f)) {
            fsm.RequestState(PetStateMachine.PetState.Walk);
        }
    }

    public void Exit() { }
}
