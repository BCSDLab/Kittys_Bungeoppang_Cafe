using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [Header("SoundSystem")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedAudioClip[] bgmClipList;
    public NamedAudioClip[] sfxClipList;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioClips();
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 삭제
        }
    }

    // 현재 씬에 맞는 BGM 호출
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    // 씬 바뀔 때 현재 BGM 해제
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // AudioClip 리스트를 딕셔너리로 변환 -> 이름으로 접근 가능
    private void InitializeAudioClips()
    {
        foreach (var bgm in bgmClipList)
        {
            if (!bgmClips.ContainsKey(bgm.name))
            {
                bgmClips.Add(bgm.name, bgm.clip);
            }
        }

        foreach (var sfx in sfxClipList)
        {
            if (!sfxClips.ContainsKey(sfx.name))
            {
                sfxClips.Add(sfx.name, sfx.clip);
            }
        }
    }

    // 배경음 재생
    public void PlayBGM(string name)
    {
        if (bgmClips.ContainsKey(name))
        {
            bgmSource.clip = bgmClips[name];
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("해당 이름의 오디오 클립이 존재하지 않습니다.");
        }
    }

    // 효과음 재생
    public void PlaySFX(string name)
    {
        if (sfxClips.ContainsKey(name))
        {
            sfxSource.clip = sfxClips[name];
            sfxSource.Play();
        }
        else
        {
            Debug.LogWarning("해당 이름의 오디오 클립이 존재하지 않습니다.");
        }
    }

    // 배경음 멈춤
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 효과음 멈춤
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    // 배경음 소리 크기 조절
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    // 효과음 소리 크기 조절
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneName == "Intro_Scene")
        {
            PlayBGM("IntroBGM");
        }
        else if (sceneName == "Play_Scene")
        {
            PlayBGM("PlayBGM");
        }
        else if (sceneName == "Ending_Best_Scene")
        {
            PlayBGM("BestBGM");
        }
        else if (sceneName == "Ending_Coin_Scene")
        {
            PlayBGM("CoinBGM");
        }
        else if (sceneName == "Ending_Fame_Scene")
        {
            PlayBGM("FameBGM");
        }
        else if (sceneName == "Ending_Bad_Scene")
        {
            PlayBGM("BadBGM");
        }
        else
        {
            StopBGM();
        }
    }
}
