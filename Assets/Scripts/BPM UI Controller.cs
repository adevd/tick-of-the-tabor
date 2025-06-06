using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BPMUIController : MonoBehaviour
{
    [SerializeField] private BPMConductor conductor;
    [SerializeField] private Slider bpmSlider;
    [SerializeField] private TextMeshProUGUI bpmText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bpmSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bpmSlider.value = conductor.BPM;
        UpdateBPMDisplay();
    }

    private void OnSliderValueChanged(float newValue)
    {
        conductor.BPM = newValue;
        UpdateBPMDisplay();

    }

    private void UpdateBPMDisplay()
    {
        bpmText.text = $"BPM: {Mathf.RoundToInt(conductor.BPM)}";
    }
}
