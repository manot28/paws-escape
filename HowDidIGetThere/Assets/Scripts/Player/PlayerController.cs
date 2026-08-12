using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("References")]
    [SerializeField] private GameObject mouseCollider;

    private Animator playerAnimator;
    private Rigidbody2D rb;

    private float inputX;
    private bool isGrounded;

    private float idleTimer;
    private const float afkTime = 10f;
    private int groundContacts;

    // to block movement while dialogue is playin
    public bool isListening;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // cant move during cutscene
        if (!isListening)
            inputX = GameInput.Instance.MoveInput;

        HandleJump();
        UpdateAnimations();
        FlipPlayer();
        MoveCollider();
        UpdateAFK();
    }

    private void FixedUpdate()
    {
        // cant move during cutscene
        if (!isListening)
        {
            rb.linearVelocity = new Vector2(
                inputX * speed,
                rb.linearVelocity.y
            );
        }
    }

    private void HandleJump()
    {
        if (GameInput.Instance.JumpPressed && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void UpdateAnimations()
    {
        playerAnimator.SetFloat("speed", Mathf.Abs(inputX));
        playerAnimator.SetFloat("velocity", rb.linearVelocity.y);
        playerAnimator.SetBool("isGround", isGrounded);
    }

    private void FlipPlayer()
    {
        if (inputX > 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputX < -0.1f)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void MoveCollider()
    {
        if (mouseCollider == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(GameInput.Instance.PointerPosition);
        mousePos.z = 0;

        mouseCollider.transform.position = mousePos;
    }

    private void UpdateAFK()
    {
        if (Mathf.Abs(rb.linearVelocity.x) < 0.01f &&
            Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0;
            playerAnimator.SetBool("AFK", false);
        }

        if (idleTimer >= afkTime)
            playerAnimator.SetBool("AFK", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            groundContacts++;
        isGrounded = groundContacts > 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            groundContacts--;
        isGrounded = groundContacts > 0;
    }

    public void Pause(int time)
    {
        Time.timeScale = time;
    }

    public void LoadNextScene(string name)
    {
        if (GameObject.FindGameObjectsWithTag("Key").Length == 0)
            SceneManager.LoadScene(name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}