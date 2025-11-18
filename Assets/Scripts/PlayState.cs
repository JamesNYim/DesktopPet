using UnityEngine;

public class PlayState : IPetState
{
    private Pet pet;
    private PetStateMachine fsm;
    private float timer;

    public PlayState(Pet pet, PetStateMachine fsm)
    {
        this.pet = pet;
        this.fsm = fsm;
    }

    public void Enter()
    {
        pet.speak("Playing");
        pet.animate("Play");
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        pet.happiness += Time.deltaTime * 10f;

        if (timer > 5f)
            fsm.RequestState(PetStateMachine.PetState.Idle);
    }

    public void Exit() { }
}
