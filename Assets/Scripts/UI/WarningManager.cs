using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : Singleton<WarningManager>
{
    public Image redOverlay;
    public float pulseSpeed = 8f;
    public int pulseCount = 8;

    private Color originalColor;
    private bool isPulsing;

    private void Start()
    {
        originalColor = redOverlay.color;
    }
    public void SendWarning()
    {
        if (!isPulsing)
        {
            StartCoroutine(PulseAlpha());
        }
    }

    private IEnumerator PulseAlpha()
    {
        isPulsing = true;
        int currentPulse = 0;

        while (currentPulse < pulseCount)
        {
            // Fade in
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * pulseSpeed;
                float alpha = Mathf.Lerp(0f, 1f, t);
                redOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // Fade out
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * pulseSpeed;
                float alpha = Mathf.Lerp(1f, 0f, t);
                redOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            currentPulse++;
        }

        // Reset to the original color
        redOverlay.color = originalColor;
        isPulsing = false;
    }
}

