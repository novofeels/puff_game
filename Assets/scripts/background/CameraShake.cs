using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeAmount = 0.1f;
    [SerializeField] float decreaseFactor = 1.0f;

    Vector3 originalPos;
    float currentShakeDuration = 0f;

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                originalPos.y + Random.insideUnitCircle.y * shakeAmount,
                originalPos.z
            );

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = new Vector3(transform.localPosition.x, originalPos.y, originalPos.z);
        }
    }

    public void ShakeCamera()
    {
        originalPos = transform.localPosition;
        currentShakeDuration = shakeDuration;
    }
}