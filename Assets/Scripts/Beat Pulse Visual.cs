using UnityEngine;

public class BeatPulseVisual : MonoBehaviour
{
    [SerializeField] private Transform pulseTarget;
    [SerializeField] private Vector3 pulseScale = new Vector3(1.3f, 1.3f, 1f);
    [SerializeField] private float pulseDuration = 0.15f;
    [SerializeField] private AnimationCurve pulseCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private Vector3 originalScale;
    private float pulseTimer = 0f;
    private bool pulsing = false;

    void Start()
    {
        originalScale = pulseTarget.localScale;
        BPMConductor.OnBeat += TriggerPulse;
    }

    private void OnDestroy()
    {
        BPMConductor.OnBeat -= TriggerPulse;
    }

    void Update()
    {
        if (!pulsing) return;

        pulseTimer += Time.unscaledDeltaTime;
        float t = pulseTimer / pulseDuration;

        if (t >= 1f)
        {
            pulsing = false;
            pulseTarget.localScale = originalScale;
        }
        else
        {
            float scaleAmount = 1f + pulseCurve.Evaluate(t) * (pulseScale.x - 1f);
            pulseTarget.localScale = originalScale * scaleAmount;
        }
    }

    void TriggerPulse()
    {
        pulseTimer = 0f;
        pulsing = true;
    }
}
