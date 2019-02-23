using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerStates playerState = PlayerStates.Gravitation;

    [Tooltip("Jump power value.")]
    [Range(0, 2000)]
    [SerializeField] private float jumpForce;
    [Tooltip("Multiply gravity value.")]
    [Range(0, 10)]
    public float gravityForceMultiplier = 1f;

    [Space]
    [SerializeField] private ParticleSystem jumpParticules;
    [SerializeField] private Color playerJumpColor;
    [SerializeField] private Color playerGravitationColor;

    private Rigidbody rb;
    private Material material;

    void Start()
    {
        // Initialize Components
        rb = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;

        // Initialize Events
        InputManager.instance.OnJump += MoveVertical;
        InputManager.instance.OnJump += ShowParticules;
        InputManager.instance.OnStopJumping += HideParticules;
        InputManager.instance.OnChangeGravity += VerticalFlip;

        // Initialize Player State
        playerState = PlayerStates.Gravitation;
        PlayerStateChange();

        // Initialize Particule effects
        HideParticules();
    }

    public void StopControls()
    {
        InputManager.instance.OnJump -= MoveVertical;
        InputManager.instance.OnJump -= ShowParticules;
        InputManager.instance.OnStopJumping -= HideParticules;
        InputManager.instance.OnChangeGravity -= VerticalFlip;
    }

    void MoveVertical()
    {
        if (playerState == PlayerStates.Jump)
        {
            int directionFactor = 1; ;
            if (!GameSystem.instance.isNormalGravity)
                directionFactor = -1;

            rb.AddForce(jumpForce * directionFactor * Vector3.up * Time.deltaTime);
        }
    }

    void VerticalFlip()
    {
        if (playerState == PlayerStates.Gravitation)
        {
            transform.Rotate(new Vector3(180f, 0f, 0f));
        }
    }

    void ShowParticules()
    {
        if (playerState == PlayerStates.Jump)
        {
            jumpParticules.Play();
        }
    }

    public void HideParticules()
    {
        jumpParticules.Stop();
    }

    public void PlayerStateChange()
    {
        if (playerState == PlayerStates.Jump)
        {
            playerState = PlayerStates.Gravitation;
            material.color = playerGravitationColor;
        }
        else if (playerState == PlayerStates.Gravitation)
        {
            playerState = PlayerStates.Jump;
            material.color = playerJumpColor;
        }
    }
}
