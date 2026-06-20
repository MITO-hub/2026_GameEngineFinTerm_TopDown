using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip clickClip;
    public AudioClip coinClip;
    public AudioClip hitClip;
    public AudioClip stageClearClip;
    public AudioClip stageFailClip;

    private bool bgmOn = true;
    private bool sfxOn = true;

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSoundSettings();
        ApplySoundSettings();
    }

    private void Start()
    {
        PlayBGM();
    }

    private void LoadSoundSettings()
    {
        bgmOn = PlayerPrefs.GetInt("BGMOn", 1) == 1;
        sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void ApplySoundSettings()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = bgmOn ? bgmVolume : 0f;
        }

        if (sfxSource != null)
        {
            sfxSource.volume = sfxOn ? sfxVolume : 0f;
        }
    }

    public void PlayBGM()
    {
        if (bgmSource == null || bgmClip == null)
            return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;

        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void PlaySFX(SfxType type)
    {
        if (!sfxOn)
            return;

        AudioClip clip = GetSFXClip(type);

        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }

    private AudioClip GetSFXClip(SfxType type)
    {
        switch (type)
        {
            case SfxType.Click:
                return clickClip;

            case SfxType.Coin:
                return coinClip;

            case SfxType.Hit:
                return hitClip;

            case SfxType.StageClear:
                return stageClearClip;

            case SfxType.StageFail:
                return stageFailClip;
        }

        return null;
    }

    public void SetBGMOn(bool isOn)
    {
        bgmOn = isOn;
        PlayerPrefs.SetInt("BGMOn", bgmOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySoundSettings();

        if (bgmOn)
            PlayBGM();
    }

    public void SetSFXOn(bool isOn)
    {
        sfxOn = isOn;
        PlayerPrefs.SetInt("SFXOn", sfxOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySoundSettings();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.Save();

        ApplySoundSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();

        ApplySoundSettings();
    }

    public bool GetBGMOn()
    {
        return bgmOn;
    }

    public bool GetSFXOn()
    {
        return sfxOn;
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}