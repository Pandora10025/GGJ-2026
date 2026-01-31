using System;
using System.Collections;
using UnityEngine;

public class MaskPreviewBaker : MonoBehaviour
{
    [Header("Offscreen Render")]
    public Camera captureCamera;              // your MaskPreviewCamera
    public RenderTexture renderTexture;       // your 512x512 RT asset

    [Header("Output")]
    public int outputSize = 512;              // should match RT size for best results

    public IEnumerator BakeAndReturn(Action<Texture2D> onDone)
    {
        if (captureCamera == null)
        {
            Debug.LogError("[MaskPreviewBaker] captureCamera not assigned.");
            onDone?.Invoke(null);
            yield break;
        }

        if (renderTexture == null)
        {
            Debug.LogError("[MaskPreviewBaker] renderTexture not assigned.");
            onDone?.Invoke(null);
            yield break;
        }

        // Ensure camera renders into the RT
        captureCamera.targetTexture = renderTexture;

        // Render after UI/layout has settled this frame
        yield return new WaitForEndOfFrame();

        // Force a render so the RT is up-to-date
        captureCamera.Render();

        // Read back from the RenderTexture into a Texture2D (keeps alpha!)
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        int w = outputSize;
        int h = outputSize;

        // If your RT isn't outputSize, use RT dimensions:
        if (renderTexture.width != outputSize || renderTexture.height != outputSize)
        {
            w = renderTexture.width;
            h = renderTexture.height;
        }

        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false, false);
        tex.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        tex.Apply();

        RenderTexture.active = previous;

        onDone?.Invoke(tex);
    }
}
