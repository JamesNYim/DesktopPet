using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("Stats")]
    public float hunger = 100f;
    public float energy = 100f;
    public float happiness = 100f;
    public float speed = 1f;

    [Header("Rates")]
    // Hunger rate
    // Energy Rate
    // Happiness Decay

    [Header("References")]
    public Animator animator;
    public Transform petTransform;
    public SpriteRenderer spriteRenderer;
    public ChatBubble chatBubble;

    private PetStateMachine fsm;

    void Awake()
    {
        fsm = new PetStateMachine(this);

    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update();

    }

    public void speak(string msg)
    {
        if (chatBubble != null)
        {
            chatBubble.Show(msg);
        }
    }

    public Vector3 GetRandomWalkPoint()
    {
        // random area around screen
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        // Example transitions

        return new Vector3(x, y, 0);
    }
}
