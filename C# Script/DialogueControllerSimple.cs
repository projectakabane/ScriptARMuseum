using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueControllerVuforia : MonoBehaviour
{
    public GameObject dialoguePanel;
    public CanvasGroup dialogueGroup; // Assign in Inspector (on dialoguePanel)
    public TextMeshProUGUI dialogueText;
    public Button toggleButton;
    public float fadeDuration = 0.3f;

    private bool isVisible = false;

    void Start()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleDialogue);

        // Initialize state (fully hidden)
        if (dialogueGroup != null)
        {
            dialogueGroup.alpha = 0f;
            dialogueGroup.interactable = false;
            dialogueGroup.blocksRaycasts = false;
        }
    }

    public void ShowDialogue(string message)
    {
        if (dialogueText != null && !string.IsNullOrEmpty(message))
            dialogueText.text = message;

        SetDialogueVisibility(true);
    }

    public void ToggleDialogue()
    {
        SetDialogueVisibility(!isVisible);
    }

    private void SetDialogueVisibility(bool show)
    {
        StopAllCoroutines();
        StartCoroutine(FadeDialogue(show));
        isVisible = show;
    }

    private IEnumerator FadeDialogue(bool show)
    {
        float time = 0f;
        float startAlpha = show ? 0f : 1f;
        float endAlpha = show ? 1f : 0f;

        if (dialogueGroup == null)
            yield break;

        // Enable interactivity when fading in
        if (show)
        {
            dialogueGroup.interactable = true;
            dialogueGroup.blocksRaycasts = true;
        }

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            dialogueGroup.alpha = alpha;
            time += Time.deltaTime;
            yield return null;
        }

        dialogueGroup.alpha = endAlpha;

        // Disable interactivity when hidden
        if (!show)
        {
            dialogueGroup.interactable = false;
            dialogueGroup.blocksRaycasts = false;
        }
    }

    public void HideDialogue()
    {
        SetDialogueVisibility(false);
    }
}
