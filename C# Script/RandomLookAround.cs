using UnityEngine;
using Vuforia;

public class ARIdleBehavior : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip idleSound;
    public string idleTriggerName = "TriggerIdle";
    private bool isVisible = false;

    private float timer;
    private float nextTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SetNextTime();

        var observer = GetComponentInParent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void Update()
    {
        if (!isVisible) return;

        timer += Time.deltaTime;

        if (timer >= nextTime)
        {
            animator.SetTrigger(idleTriggerName);

            if (idleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(idleSound, 1.0f);
            }

            timer = 0f;
            SetNextTime();
        }
    }

    void SetNextTime()
    {
        nextTime = Random.Range(2f, 5f);
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isVisible = status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED;
    }
}
