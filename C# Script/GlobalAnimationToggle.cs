using UnityEngine;
using UnityEngine.UI;

public class GlobalAnimationToggle : MonoBehaviour
{
    private bool isPlaying = true;
    private ModelAnimationController[] allControllers;

    [Header("UI")]
    public Button toggleButton;
    public Image buttonImage; // This is the Image component on the Button
    public Sprite playIcon;
    public Sprite pauseIcon;

    void Start()
    {
        allControllers = FindObjectsOfType<ModelAnimationController>();
        UpdateButtonIcon();

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleAnimations);
        }
    }

    public void ToggleAnimations()
    {
        isPlaying = !isPlaying;

        foreach (var controller in allControllers)
        {
            if (controller.isTracked)
            {
                controller.SetAnimationPlaying(isPlaying);
            }
        }

        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = isPlaying ? pauseIcon : playIcon;
        }
    }
}