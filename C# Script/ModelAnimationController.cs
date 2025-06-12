using UnityEngine;
using Vuforia;

public class ModelAnimationController : MonoBehaviour
{
    public Animator animator;
    private ObserverBehaviour observerBehaviour;
    public bool isTracked = false;

    private void Awake()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isTracked = status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED;
    }

    public void SetAnimationPlaying(bool play)
    {
        if (animator != null)
        {
            animator.speed = play ? 1f : 0f;
        }
    }
}
