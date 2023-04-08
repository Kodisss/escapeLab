using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject greenLight;

    private bool initializeLights = true;

    private GameScript game;

    [SerializeField] private bool safe;

    // Start is called before the first frame update
    void Start()
    {
        redLight.SetActive(false);
        greenLight.SetActive(false);
        
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.GetLights() && initializeLights)
        {
            if (safe)
            {
                greenLight.SetActive(true);
            }
            else
            {
                redLight.SetActive(true);
            }
            initializeLights = false;
        }
    }

    // display message when close
    private void OnTriggerEnter(Collider playerCollider)
    {
        if (!safe)
        {
            game.SetAlive(false);
        }
    }
}
