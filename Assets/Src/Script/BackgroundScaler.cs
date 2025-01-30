using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(0.5f, 0.5f, 1f); // Final scale of the background
    public float duration = 5f; // Duration of the scaling animation in seconds

    private Vector3 initialScale;
    private float elapsedTime = 0f;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            // Smoothly interpolate the scale from initial to target
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
    }
}
