using UnityEngine;
using System.Collections.Generic;

public class PetStateMachine
{
    private Pet pet;
    private IPetState currentState;
    private Dictionary<PetState, IPetState> states;

    public enum PetState { Idle, Walk, Sleep, Play }

    public PetStateMachine(Pet pet)
    {
        this.pet = pet;
        states = new Dictionary<PetState, IPetState>
        {
            { PetState.Idle, new IdleState(pet, this) },
            { PetState.Walk, new WalkState(pet, this) },
            { PetState.Sleep, new SleepState(pet, this) },
            { PetState.Play, new PlayState(pet, this) },
        };

        ChangeState(PetState.Idle);
    }

    public void Update()
    {
        currentState.Update();
    }

    public void ChangeState(PetState newState)
    {
        //currentState.Exit();
        currentState = states[newState];
        currentState.Enter();
    }
}

