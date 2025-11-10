using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public CanvasGroup canvasGroup;

    public float fadeDuration = 0.5f;
    public float visibleTime = 3f;

    private float timer;
    
    // Shows the message
    public void Show(string msg)
    {
        messageText.text = msg;
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        timer = visibleTime;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(FadeOut());
            }
        }

    }

    private System.Collections.IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1 - t / fadeDuration;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
