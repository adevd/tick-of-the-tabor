using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private InputSystem_Actions inputActions;
    private Vector2 lastMoveInput = Vector2.zero;
    private bool hasTriggeredSpin = false;


    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Attack.performed += OnAttackPerformed;
        inputActions.Player.Move.performed += OnMovePerformed;

    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Player.Move.performed -= OnMovePerformed;

        inputActions.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("Jump");
    }
    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        animator.SetTrigger("Swing");
    }
    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        if (!hasTriggeredSpin && input.sqrMagnitude > 0.2f)
        {
            animator.SetTrigger("Spin");
            hasTriggeredSpin = true;
        }

        if (input.sqrMagnitude < 0.05f)
        {
            hasTriggeredSpin = false;
        }

        lastMoveInput = input;
    }


}
