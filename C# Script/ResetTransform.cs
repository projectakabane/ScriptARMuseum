using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Start()
    {
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    public void ResetTransformState()
    {
        transform.localScale = originalScale;
        transform.localRotation = originalRotation;
    }
}
