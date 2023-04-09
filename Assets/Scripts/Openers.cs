using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openers : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected GameScript game;
    [SerializeField] protected string triggerName;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        TriggerAnimation();
    }

    protected virtual void TriggerAnimation()
    {
        animator.SetTrigger(triggerName);
    }
}
