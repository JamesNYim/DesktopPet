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
        pet.speak("Sleeping...");
        pet.animator.Play("Sleep");
    }

    public void Update()
    {
        pet.energy += Time.deltaTime * 10f;
        if (pet.energy >= 90f)
        {
            fsm.ChangeState(PetStateMachine.PetState.Idle);
        }
    }

    public void Exit()
    {
        // maybe wake up animation later
    }
}
