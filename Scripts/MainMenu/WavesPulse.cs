using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesPulse : MonoBehaviour
{
    public Image image;
    public float waveSpeed = 1.0f;
    public float glowIntensity = 1.0f;
    public Color baseColor = Color.white;

    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    private void Update()
    {
        // Calculate the pulsing effect using Mathf.PingPong to create a wave motion
        float pulse = Mathf.PingPong(Time.time * waveSpeed, 1.0f);

        // Calculate the new color with increased intensity based on the pulsing effect
        Color targetColor = baseColor * (1 + pulse * glowIntensity);

        // Apply the new color to the image
        image.color = targetColor;
    }
}
