using UnityEngine;

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip beatClip;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        BPMConductor.OnBeat += PlayClick;
    }

    private void OnDisable()
    {
        BPMConductor.OnBeat -= PlayClick;
    }

    private void PlayClick()
    {
        if (beatClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(beatClip);
        }
    }
}
