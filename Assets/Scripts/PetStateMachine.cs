using UnityEngine;
using System.Collections.Generic;

public class PetStateMachine
{
    private Pet pet;
    private IPetState currentState;
    private PetState currentStateType;
    private Dictionary<PetState, IPetState> states;

    public enum PetState { Idle, Walk, Sleep, Play, Menu }

    public PetStateMachine(Pet pet)
    {
        this.pet = pet;
        states = new Dictionary<PetState, IPetState>
        {
            { PetState.Idle, new IdleState(pet, this) },
            { PetState.Walk, new WalkState(pet, this) },
            { PetState.Sleep, new SleepState(pet, this) },
            { PetState.Play, new PlayState(pet, this) },
            { PetState.Menu, new MenuState(pet, this) },
        };

        ChangeState(PetState.Idle);
    }

    public void Update()
    {
        currentState.Update();
    }

    public void RequestState(PetState requestedState)
    {
        if (canTransitionTo(requestedState))
        {
            ChangeState(requestedState);
        }
        else
        {
            pet.speak("Cannot transition from: " + currentState + " to " + requestedState);
        }
    }

    private bool canTransitionTo(PetState newState)
    {
        switch (newState)
        {
            case PetState.Sleep:
                return pet.energy < 20;

            case PetState.Play:
                return pet.happiness < 80;
        
            case PetState.Menu:
                return true;

            case PetState.Walk:
                return currentStateType != PetState.Sleep;

            case PetState.Idle: 
                return true;
        }

        return false;

    }


    private void ChangeState(PetState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = states[newState];
        currentStateType = newState;
        currentState.Enter();
    }

}

