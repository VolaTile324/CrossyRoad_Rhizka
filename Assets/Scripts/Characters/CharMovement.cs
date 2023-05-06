using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class CharMovement : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float moveDuration = 0.1f;
    [SerializeField, Range(0, 1)] float jumpHeight = 0.5f;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    // Get the prefab
    public GameObject prefab;
    // Game camera that will follow the player, everyone hold hands :)
    public Camera gameCamera;
    // player spawn event
    public UnityEvent OnSpawn;
    // check jump conditon
    public UnityEvent OnJumpStart;
    public UnityEvent<Vector3> OnJumpEnd;
    // check coin collision
    public UnityEvent<int> OnCollectCoin;
    // player death event
    public UnityEvent OnDeath;
    // player death by whatever that kills the player
    public UnityEvent OnDeathByCar;
    public UnityEvent OnDeathByEagle;
    // check if player cant move
    private bool isUnplayable = false;
    // check pause
    public UnityEvent OnPause;
    private bool isPaused = false;

    public void SpawnFX()
    {
        // character will fadein via Y scale, like the opposite of being flattened :D
        prefab.transform.localScale = Vector3.zero;
        prefab.transform.DOScale(0.5f, 1);
    }

    private void Start()
    {
        OnSpawn.Invoke();
    }

    void Update()
    {
        // check if player cant move
        if (isUnplayable == true)
        {
            return;
        }
        
        // prevent player doing input until the movement is complete
        if(DOTween.IsTweening(transform))
        {
            return;
        }

        // prevent input during game pause
        if (isPaused == true)
        {
            return;
        }

        Vector3 direction = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.forward;
            // character face forward
            prefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.back;
            // character face backward
            prefab.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
            // character face left
            prefab.transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            // character face right
            prefab.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause.Invoke();
        }

        if (direction == Vector3.zero)
        {
            return;
        }

        Move(direction);
    }

    public void PauseGame()
    {
        // check if the game is paused
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;

        }
    }

    public void Move(Vector3 direction)
    {
        // event whenever player jumped
        OnJumpStart.Invoke();

        // check if the player is going to move out of the map
        var targetPos = transform.position + direction;
        if (targetPos.x < leftMoveLimit || 
            targetPos.x > rightMoveLimit || 
            targetPos.z < backMoveLimit || 
            Tree.AllPositions.Contains(targetPos))
        {
            targetPos = transform.position;
        }
        
        // this define the hopping movement animation, HOP :D
        prefab.transform.DOJump(
            targetPos,
            jumpHeight,
            1,
            moveDuration
            )
            .onComplete = BroadcastPositionOnJumpEnd;
        transform.forward = direction;
    }

    // set char playable or not
    public void SetUnplayable(bool value)
    {
        isUnplayable = value;
    }

    // this is to update the map size
    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize / 2;
        rightMoveLimit = horizontalSize / 2;
        backMoveLimit = backLimit;
    }
    
    // this is to update position
    private void BroadcastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    // collision triggers
    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Car"))
        {
            if (isUnplayable == true)
            {
                return;
            }
            Debug.Log("duck and car collided, game should be over");
            // lets disable eagle spawner since the char died
            var eagleSpawner = GameObject.Find("EagleSpawner");
            eagleSpawner.GetComponent<EagleSpawner>().enabled = false;
            // flatten the poor little one :(
            prefab.transform.DOScaleY(0.1f, 0.2f);
            // on death by car event, play the confetti particle
            isUnplayable = true;
            OnDeathByCar.Invoke();
            Invoke("PlayerDeath", 2);
        }
        else if (other.CompareTag("Coin"))
        {
            if (isUnplayable == true)
            {
                return;
            }
            Debug.Log("duck and coin collided, coin should be collected");
            // destroy the coin
            var coinVar = other.GetComponent<Coin>();
            OnCollectCoin.Invoke(coinVar.CoinValue);
            coinVar.Collected();
        }
        else if (other.CompareTag("Eagle"))
        {
            Debug.Log("duck and eagle collided, game should be over");
            // the little one becomes the eagle's dinner :(
            if(this.transform != other.transform)
            {
                this.transform.SetParent(other.transform);
                isUnplayable = true;
                Invoke("PlayerDeath", 2);
                // once it catches player, the eagle will fly away, rotating X around 30 degrees
                other.transform.DORotate(new Vector3(30, 0, 0), 0.4f);
                // on death by eagle event, we need to update the death reason
                OnDeathByEagle.Invoke();

            }
        }
    }

    private void PlayerDeath()
    {
        OnDeath.Invoke();
        // save score
        PlayerPrefs.Save();
    }
}
