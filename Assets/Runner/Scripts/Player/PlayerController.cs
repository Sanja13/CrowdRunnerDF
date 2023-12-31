using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator animator;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth;
    private bool canMove;

    [Header(" Contorol ")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        GameManager.onGameStateChange += GameStateChangeCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangeCallback;
    }


    void Update()
    {
        if (canMove)
        {
           MoveForward();
           ManageControl();
        }
        
    }
    private void GameStateChangeCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
            StartMoving();
        else if(gameState == GameManager.GameState.Gameover|| gameState == GameManager.GameState.LevelComplete)
                EndMoving();
    }
    private void StartMoving()
    {
        canMove = true;
        animator.Run();
    }
    private void EndMoving()
    {
        canMove = false;
        animator.Idle();
    }
    private void MoveForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(),
                roadWidth / 2 - crowdSystem.GetCrowdRadius());
            transform.position = position;
        }
    }
}
