using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("SFX")]
    public AudioClip playerHitSFX; // ✅ 공통 플레이어 피격음
    public AudioClip enemyHitSFX;  // ✅ 공통 적 피격음

    [Header("BGM")]
    public AudioClip fieldBGM;
    public AudioClip miniGameBGM;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    [Header("Area BGM")]
    public AudioClip mainSceneBGM;
    public AudioClip area1BGM;
    public AudioClip area2BGM;
    public AudioClip area3BGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bgmSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (currentScene == "MainScene" && mainSceneBGM != null)
        {
            PlayBGM(mainSceneBGM);
        }
        else if (fieldBGM != null)
        {
            PlayBGM(fieldBGM);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("🎵 씬 로드됨: " + scene.name);

        if (scene.name == "MainScene" && mainSceneBGM != null)
        {
            Debug.Log("🎵 메인씬 BGM 재생 시작");
            PlayBGM(mainSceneBGM);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip != null && bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

  
public void SetMasterVolume(float value)
{
    AudioListener.volume = Mathf.Clamp01(value);
    Debug.Log($"🎚️ 마스터 볼륨 설정: {AudioListener.volume}");
}


public void SetBGMVolume(float value)
{
    if (bgmSource != null)
    {
        bgmSource.volume = Mathf.Clamp01(value);
        Debug.Log($"🎼 BGM 볼륨 설정: {bgmSource.volume}");
    }
}


public void SetSFXVolume(float value)
{
    if (sfxSource != null)
    {
        sfxSource.volume = Mathf.Clamp01(value);
        Debug.Log($"🎯 SFX 볼륨 설정: {sfxSource.volume}");
    }
}

    public void PlayAreaBGM(AudioClip areaClip)
    {
        if (areaClip != null && bgmSource.clip != areaClip)
        {
            bgmSource.clip = areaClip;
            bgmSource.Play();
        }
    }


}
