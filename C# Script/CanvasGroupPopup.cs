using UnityEngine;
using System.Collections;

public class UIPopupToggle : MonoBehaviour
{
    public GameObject popupObject;
    public float fadeDuration = 0.3f;

    private bool isVisible = false;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = popupObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popupObject.AddComponent<CanvasGroup>();
        }

        // Start with invisible and non-interactable
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Keep the object active so the script can work on it
        popupObject.SetActive(true);
    }

    public void TogglePopup()
    {
        StopAllCoroutines();
        StartCoroutine(FadePopup(isVisible ? 1 : 0, isVisible ? 0 : 1));
        isVisible = !isVisible;
    }

    private IEnumerator FadePopup(float from, float to)
    {
        float time = 0f;

        if (to > from)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;

        if (to == 0)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            // Do NOT set popupObject inactive — keep it active!
        }
    }
}
