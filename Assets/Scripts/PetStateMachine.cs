using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;

public class PetStateMachine {
    public enum PetState { Idle, Walk, Sleep, Play }

    private Pet pet;
    private PetState state = PetState.Idle;
    private float stateTimer = 0f;

    public PetStateMachine(Pet pet)
    {
        this.pet = pet;
        EnterState(PetState.Idle);
    }

    public void Update()
    {
        stateTimer += Time.deltaTime;

        switch (state)
        {
            case PetState.Idle:
                IdleUpdate();
                break;
            case PetState.Walk:
                WalkUpdate();
                break;
            case PetState.Sleep:
                SleepUpdate();
                break;
            case PetState.Play:
                PlayUpdate();
                break;
        }
        // Check for state change
        checkState();
    }

    private void checkState()
    {
        // Sleep State
        // Possible different energy threshholds for different pets?
        if (pet.energy < 20 && state != PetState.Sleep)
        {
            changeState(PetState.Sleep);
        }

        // Play State
        else if (pet.happiness < 20 && state != PetState.Play)
        {
            changeState(PetState.Play);
        }
        // Walk State
        else if (state == PetState.Idle && stateTimer > Random.Range(5f, 10f))
        {
            changeState(PetState.Walk);
        }

        // Idle State
        else if (state == PetState.Walk && stateTimer > Random.Range(1f, 5f))
        {
            changeState(PetState.Idle);
        }
    }

    private void changeState(PetState newState)
    {
        ExitState(state);
        EnterState(newState);
    }

    private void EnterState(PetState newState)
    {
        state = newState;
        stateTimer = 0f;

        switch (state)
        {
            case PetState.Sleep:
                pet.speak("Sleeping...");
                pet.animator.Play("Sleep");
                break;
            case PetState.Play:
                pet.speak("Playing...");
                pet.animator.Play("Play");
                break;
            case PetState.Walk:
                pet.speak("Walking...");
                pet.animator.Play("Walk");
                break;
            case PetState.Idle:
                pet.speak("Idling...");
                pet.animator.Play("Idle");
                break;
        }
    }

    private void ExitState(PetState oldState)
    {
        switch (state)
        {
            case PetState.Sleep:
                break;
            case PetState.Play:
                break;
            case PetState.Walk:
                break;
            case PetState.Idle:
                break;
        }
    }

    private void IdleUpdate()
    {
        // Idle Action
    }

    private void WalkUpdate()
    {
        // Walking behavior
        float dir = 1;
        pet.petTransform.position += Vector3.right * dir * 0.0001f;
        if (dir > 0)
        {
            pet.spriteRenderer.flipX = false;
        }
        else
        {
            pet.spriteRenderer.flipX = true;
        }
        pet.energy -= Time.deltaTime * 10f;
    }

    private void PlayUpdate()
    {
        // Play action
        // Play with mouse
        // Play with windows
        // *Should this be a different state?*
    }

    private void SleepUpdate()
    {
        pet.energy += Time.deltaTime * 10f;
        if (pet.energy >= 90f)
        {
            changeState(PetState.Idle);
        }
    }
}
