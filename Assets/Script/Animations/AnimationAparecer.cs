using System.Collections;
using UnityEngine;

public class AnimationAparecer : MonoBehaviour
{
    public float duration = 0.6f;

    void OnEnable()
    {
        StartCoroutine(AnimateAppear());
    }

    IEnumerator AnimateAppear()
    {
        transform.localScale = Vector3.zero;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            // Curva suave
            float scale = Mathf.SmoothStep(0f, 1f, normalized);

            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
