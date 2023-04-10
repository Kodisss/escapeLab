using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimonButton : InteractObjects
{
    [SerializeField] private Light myLight;
    [SerializeField] private string buttonName; // 1 = green, 2 = red, 3 = blue
    private SimonSaysScript simonSays;

    protected override void Start()
    {
        base.Start();
        simonSays = GameObject.FindGameObjectWithTag("SimonSays").GetComponent<SimonSaysScript>();
    }

    protected override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            Activate();
        }
    }

    protected override void Activate()
    {
        StartCoroutine(Sparkles());
        simonSays.SendInput(buttonName);
    }

    private IEnumerator Sparkles()
    {
        myLight.intensity += 100f;
        yield return new WaitForSeconds(0.5f);
        myLight.intensity -= 100f;
    }
}
