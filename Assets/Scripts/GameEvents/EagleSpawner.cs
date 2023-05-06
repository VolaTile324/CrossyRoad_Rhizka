using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    // handle eagle npc and player target
    [SerializeField] private Eagle eagleObj;
    [SerializeField] private CharMovement playerTarget;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip eagleSound;

    // handle timer that spawns eagles
    [SerializeField] private float intialGameTimer;
    float gameTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        gameTimer = intialGameTimer;
        eagleObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer <= 0 && eagleObj.gameObject.activeInHierarchy == false)
        {
            // spawn eagle
            eagleObj.gameObject.SetActive(true);
            // set eagle to track player position
            eagleObj.transform.position = playerTarget.transform.position + new Vector3(0, 0.6f, 10);
            // lock the little one, since it'll die anyway
            playerTarget.SetUnplayable(true);
            // play eagle sound
            audioManager.PlaySFX(eagleSound);
            // stop the eagle sound
            eagleSound = null;
        }
        
        gameTimer -= Time.deltaTime;
    }

    // function to reset timer everytime the terrain updates
    public void ResetTimer()
    {
        gameTimer = intialGameTimer;
    }
}
