using UnityEngine;
using System;

public class BPMConductor : MonoBehaviour
{
    [Header("Beat Settings")]
    [SerializeField] private float bpm;
    public float BPM
    {
        get => bpm;
        set
        {
            bpm = value;
            beatInterval = 60f / bpm;
        }
    }

    [Header("Debug")]
    [SerializeField] private bool printBeats = true;

    public static event Action OnBeat;

    private float beatInterval;
    private float nextBeatTime;
    private float songPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatInterval = 60f / bpm;
        nextBeatTime = Time.time + beatInterval;
        songPosition = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition += Time.unscaledDeltaTime;

        if (Time.unscaledTime >= nextBeatTime)
        {
            if (printBeats)
            {
                Debug.Log($"🪘 Beat at {songPosition:F3}s");

            }
            OnBeat?.Invoke();
            nextBeatTime += beatInterval;
        }
    }

    public float GetTimeSinceLastBeat() => Time.unscaledTime - (nextBeatTime - beatInterval);
    public float GetTimeToNextBeat() => nextBeatTime - Time.unscaledTime;
    public float GetBeatProgress() => GetTimeSinceLastBeat() / beatInterval;

    public float GetPreviousBeatTime() => nextBeatTime - beatInterval;
}
