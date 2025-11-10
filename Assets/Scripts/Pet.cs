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
    public float speed = 100f;

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
}
