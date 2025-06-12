using UnityEngine;
using Vuforia;

public class MarkerHandler : MonoBehaviour
{
    public string messageToShow = "You scanned: Marker A";

    private DialogueControllerVuforia dialogue;

    void Start()
    {
        dialogue = FindObjectOfType<DialogueControllerVuforia>();

        var observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            if (dialogue != null)
                dialogue.ShowDialogue(messageToShow);
        }
        else
        {
            if (dialogue != null)
                dialogue.HideDialogue();
        }
    }
}
