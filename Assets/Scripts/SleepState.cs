using UnityEngine;

public class SleepState : IPetState
{
    private Pet pet;
    private PetStateMachine fsm;

    public SleepState(Pet pet, PetStateMachine fsm)
    {
        this.pet = pet;
        this.fsm = fsm;
    }

    public void Enter()
    {
        pet.speak("Sleeping");
        pet.animate("Sleep");
    }

    public void Update()
    {
        pet.stats.Sleep(Time.deltaTime);
        if (pet.stats.energy >= 90f)
        {
            fsm.RequestState(PetStateMachine.PetState.Idle);
        }
    }

    public void Exit()
    {
        // maybe wake up animation later
    }
}
