using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton
    public static InputManager instance;

    // Action
    public event Action OnChangeGravity = delegate { };
    public event Action OnJump = delegate { };
    public event Action OnStopJumping = delegate { };

    // Inputs Code
    [SerializeField] private KeyCode action;

    // Initialize Singleton
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (GameSystem.gameState == GameStates.Playing)
        {
            // Input for IOS and Android platforms
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        OnChangeGravity();
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Stationary:
                        OnJump();
                        break;

                    case TouchPhase.Ended:
                        OnStopJumping();
                        break;
                }
            }
#endif

            // Input for WIN and OSX platforms
# if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN

            if (Input.GetKeyDown(action))
                OnChangeGravity();

            if (Input.GetKey(action))
                OnJump();

            if (!Input.GetKey(action))
                OnStopJumping();
#endif
        }
    }
}
