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
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ����
        }
    }

    // ���� ���� �´� BGM ȣ��
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    // �� �ٲ� �� ���� BGM ����
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // AudioClip ����Ʈ�� ��ųʸ��� ��ȯ -> �̸����� ���� ����
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

    // ����� ���
    public void PlayBGM(string name)
    {
        if (bgmClips.ContainsKey(name))
        {
            bgmSource.clip = bgmClips[name];
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("�ش� �̸��� ����� Ŭ���� �������� �ʽ��ϴ�.");
        }
    }

    // ȿ���� ���
    public void PlaySFX(string name)
    {
        if (sfxClips.ContainsKey(name))
        {
            sfxSource.clip = sfxClips[name];
            sfxSource.Play();
        }
        else
        {
            Debug.LogWarning("�ش� �̸��� ����� Ŭ���� �������� �ʽ��ϴ�.");
        }
    }

    // ����� ����
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ȿ���� ����
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    // ����� �Ҹ� ũ�� ����
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    // ȿ���� �Ҹ� ũ�� ����
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
