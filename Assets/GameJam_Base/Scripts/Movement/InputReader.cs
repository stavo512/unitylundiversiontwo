using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance;

    [SerializeField] private InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction jump;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        var gameplayMap = inputActions.FindActionMap("Gameplay");

        moveAction = gameplayMap.FindAction("Move");
        attackAction = gameplayMap.FindAction("Attack");
        jump = gameplayMap.FindAction("Jump");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        attackAction.Disable();
        jump.Disable();
    }

    // public API

    public Vector2 Move => moveAction.ReadValue<Vector2>();

    public bool AttackHeld => attackAction.IsPressed();

    public bool AttackPressed => attackAction.WasPressedThisFrame();
    public bool JumpPressed => jump.WasPressedThisFrame();
    public bool JumpHeld => jump.IsPressed();
}
