using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmJudge : MonoBehaviour
{
    [SerializeField] private BPMConductor conductor;

    [Header("Judgement Windows (seconds)")]
    [SerializeField] private float perfectThreshold = 0.05f;
    [SerializeField] private float goodThreshold = 0.12f;
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform popupSpawnPoint;

    private InputAction jumpAction;
    private InputSystem_Actions inputActions;

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        jumpAction = inputActions.Player.Jump;
        jumpAction.performed += OnJumpJudgeBeatTiming;
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnJumpJudgeBeatTiming;
        inputActions.Disable();
    }

    private void OnJumpJudgeBeatTiming(InputAction.CallbackContext ctx)
    {
        float inputTime = (float)ctx.time;
        float beatTime = Time.unscaledTime - conductor.GetTimeSinceLastBeat();
        float offset = Mathf.Abs(inputTime - beatTime);

        if (offset <= perfectThreshold)
        {
            string perfectMessage = "🌞 Perfect!";
            ShowPopup(perfectMessage, Color.green);
        }
        else if (offset <= goodThreshold)
        {
            string goodMessage = "👌 Good!";
            ShowPopup(goodMessage, Color.blue);
        }
        else
        {
            string fineMessage = "🌱 Nice Try!";
            ShowPopup(fineMessage, Color.magenta);
        }
    }

    private string GenerateJudgement(string comment, float offset)
    {
        return $"{comment} Offset: {offset:F3}s";
    }

    private void ShowPopup(string text, Color color)
    {
        var popup = Instantiate(popupPrefab, popupSpawnPoint);
        popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -100f);
        popup.GetComponent<JudgementPopup>().Setup(text, color);
    }
}
