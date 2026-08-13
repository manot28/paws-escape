using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public float MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public Vector2 PointerPosition { get; private set; }

    private bool mobileJump;
    private bool mobileInteract;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS

        PointerPosition = Input.touchCount > 0
            ? Input.GetTouch(0).position
            : Vector2.zero;

        JumpPressed = mobileJump;
        InteractPressed = mobileInteract;

        mobileJump = false;
        mobileInteract = false;

#else
        // pc
        MoveInput = Input.GetAxisRaw("Horizontal");

        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        InteractPressed = Input.GetKeyDown(KeyCode.E);

        PointerPosition = Input.mousePosition;

#endif
    }

    // mobile ui buttons

    public void SetMove(float value)
    {
        MoveInput = value;
    }

    public void StopMove()
    {
        MoveInput = 0f;
    }

    public void PressJump()
    {
        mobileJump = true;
    }

    public void PressInteract()
    {
        mobileInteract = true;
    }
}