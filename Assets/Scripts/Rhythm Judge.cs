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
    private static readonly Color perfectColour = Hex("#488B49");
    private static readonly Color goodColour = Hex("#476C9B");
    private static readonly Color fineColour = Hex("#8E8E8E");

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
        float interval = 60f / conductor.BPM;

        float previousBeatTime = conductor.GetPreviousBeatTime();
        float nextBeatTime = previousBeatTime + interval;

        float offsetToPreviousBeat = Mathf.Abs(inputTime - previousBeatTime);
        float offsetToNextBeat = Mathf.Abs(inputTime - nextBeatTime);

        bool closerToPrevious = offsetToPreviousBeat <= offsetToNextBeat;
        float closestOffset = closerToPrevious ? offsetToPreviousBeat : offsetToNextBeat;
        string direction = closerToPrevious ? "Dragging" : "Rushing";

        string judgement;
        Color color;

        if (closestOffset <= perfectThreshold)
        {
            judgement = $"🌞 Perfect!";
            color = perfectColour;

        }
        else if (closestOffset <= goodThreshold)
        {
            judgement = "👌 Good!";
            color = goodColour;
        }
        else
        {
            judgement = "🌱 Nice Try!";
            color = fineColour;
        }
        ShowPopup(GenerateJudgement(judgement, closestOffset, direction), color);

    }

    private string GenerateJudgement(string comment, float offset, string direction)
    {
        return $"{comment} \n{direction}: {offset:F3}s";
    }

    private void ShowPopup(string text, Color color)
    {
        var popup = Instantiate(popupPrefab, popupSpawnPoint);
        popup.GetComponent<RectTransform>().anchoredPosition = new Vector2(500f, -200f);
        popup.GetComponent<JudgementPopup>().Setup(text, color);
    }

    private static Color Hex(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

}
