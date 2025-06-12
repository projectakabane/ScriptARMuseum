using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider;
    public Button muteButton;
    public Image muteIconImage; // Assign the child icon image here
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    private bool isMuted = false;

    void Start()
    {
        // Safely get volume from BackgroundMusic
        if (BackgroundMusic.Instance != null)
        {
            float currentVolume = BackgroundMusic.Instance.GetVolume();
            volumeSlider.value = currentVolume;
            isMuted = BackgroundMusic.Instance.IsMuted();
        }

        // Add listeners
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteButton.onClick.AddListener(ToggleMute);

        UpdateMuteIcon();
    }

    public void SetVolume(float volume)
    {
        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.SetVolume(volume);

            isMuted = Mathf.Approximately(volume, 0f);
            BackgroundMusic.Instance.Mute(isMuted);
            UpdateMuteIcon();
        }
    }

    public void ToggleMute()
    {
        if (BackgroundMusic.Instance == null) return;

        isMuted = !isMuted;
        BackgroundMusic.Instance.Mute(isMuted);

        // If muting, remember last volume; if unmuting, restore default or slider value
        float volumeToSet = isMuted ? 0f : volumeSlider.value > 0 ? volumeSlider.value : 1f;
        BackgroundMusic.Instance.SetVolume(volumeToSet);
        volumeSlider.value = volumeToSet;

        UpdateMuteIcon();
    }

    private void UpdateMuteIcon()
    {
        if (muteIconImage != null)
        {
            muteIconImage.sprite = isMuted ? muteSprite : unmuteSprite;
        }
    }
}
