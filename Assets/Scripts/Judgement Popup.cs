using UnityEngine;
using TMPro;

public class JudgementPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float floatDistance = 50f;

    private Vector3 startPos;

    public void Setup(string message, Color color)
    {
        text.text = message;
        text.color = color;
        canvasGroup.alpha = 1f;
        startPos = transform.position;
        StartCoroutine(FadeAndFloat());
    }

    private System.Collections.IEnumerator FadeAndFloat()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            float easeT = 1f - Mathf.Pow(1f - t, 2);
            transform.position = startPos + Vector3.up * floatDistance * easeT;
            canvasGroup.alpha = 1f - t;
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
