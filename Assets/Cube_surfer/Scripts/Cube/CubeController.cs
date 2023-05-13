using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private HeroStackController heroStackController;
    private Vector3 direction = Vector3.back;
    private bool isStack = false;
    private RaycastHit hit;
    private HeroMovementController heroMovement;
    private GameManager _gameManager;
    public static int score;

    private void Start()
    {
        heroStackController = GameObject.FindObjectOfType<HeroStackController>();
        heroMovement = GameObject.FindObjectOfType<HeroMovementController>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="cube")
        {
            if (!isStack)
            {
                isStack = !isStack;
                heroStackController.IncreaseNewBlock(gameObject);
                SetDirection();
            }
        } 

        if(other.gameObject.tag=="Obstacle")
        {
          heroStackController.DecreaseBlock(gameObject);
        }

        if(other.gameObject.tag == "left")
        {
            heroMovement.turnLeft();
        }

        if(other.gameObject.tag == "right")
        {
            heroMovement.turnRight();
        }

        if(other.gameObject.tag == "road")
        {
            heroMovement.roadFunc(other.gameObject);
        }

        if(other.gameObject.tag == "finish")
        {
            heroMovement.resetSpeed();
            _gameManager.nextLevel();
        }
    }

    private void SetDirection()
    {
        direction = Vector3.forward;
    }
}
