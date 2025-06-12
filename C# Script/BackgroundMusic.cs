using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }

    private AudioSource bgmSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Kill duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Get and play the AudioSource
        bgmSource = GetComponent<AudioSource>();

        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else if (bgmSource == null)
        {
            Debug.LogWarning("BackgroundMusic: No AudioSource component found!");
        }
    }

    public void SetVolume(float volume)
    {
        if (bgmSource != null)
            bgmSource.volume = volume;
    }

    public void Mute(bool mute)
    {
        if (bgmSource != null)
            bgmSource.mute = mute;
    }

    public float GetVolume()
    {
        return bgmSource != null ? bgmSource.volume : 0f;
    }

    public bool IsMuted()
    {
        return bgmSource != null && bgmSource.mute;
    }
}
