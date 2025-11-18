using UnityEngine;

[System.Serializable]
public class PetStats 
{
    // Stats
    public float hunger = 100;
    public float happiness = 100;
    public float energy = 100;
    public float xp = 0;

    public void Tick(float dt = 1f)
    {
        float expPerSecond = 1f / 60f;
        AddExp(dt * expPerSecond);
    }
    // public actions
    public void AddExp(float amount)
    {
        xp += amount;
        // CheckLevelUp()
    }
    public void Feed(float amount)
    {
        ChangeHunger(+amount);
        ChangeHappiness(+2);
    }

    public void Play(float amount)
    {
        ChangeHappiness(+amount);
        ChangeEnergy(-10);
    }

    public void Sleep(float amount)
    {
        ChangeEnergy(+amount * 2);
        ChangeHunger(-5);
    }

    public void Walk(float amount)
    {
        ChangeEnergy(-amount);
        ChangeHunger(-3);
    }

    public void Idle(float amount)
    {
        ChangeEnergy(-amount);
    }
    
    // Changing Stats
    private void ChangeHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0, 100);
    }

    private void ChangeHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    private void ChangeEnergy(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, 100);
    }
}

