using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonAnswer : MonoBehaviour
{
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject blueLight;
    [SerializeField] private GameObject redLight;

    [SerializeField] private Collider playerCollider;

    private GameScript game;

    private bool playerInRange = false;
    private bool initialize = true;

    // Start is called before the first frame update
    private void Start()
    {
        redLight.SetActive(false);
        blueLight.SetActive(false);
        greenLight.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!game.GetLights())
        {
            if (initialize && playerInRange)
            {
                StartCoroutine(LightShow()); // start sparkling in the right order
                initialize = false; // don't repeat the starting process
            }
            else if(!playerInRange)
            {
                initialize = true;
            }
        }
    }

    // display message when close
    protected virtual void OnTriggerEnter(Collider playerCollider)
    {
        playerInRange = true;
    }

    // deletes message when leaving
    protected virtual void OnTriggerExit(Collider playerCollider)
    {
        playerInRange = false;
    }

    // spakles yayyy
    private IEnumerator LightShow()
    {
        // stops when the player isn't here to save memory
        while (playerInRange)
        {
            yield return new WaitForSeconds(2.0f);
            redLight.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            redLight.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            greenLight.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            greenLight.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            blueLight.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            blueLight.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
