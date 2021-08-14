using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SoundType
{
    BGM,
    EFFECT,
    END,
}

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSources = new AudioSource[(int)SoundType.END];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    Queue<AudioClip> effect_clips = new Queue<AudioClip>();

    public static SoundManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            GameObject sound = GameObject.Find("Sound");

            if (sound)
            {
                name = "Sound";
                DontDestroyOnLoad(gameObject);

                string[] soundNames = Enum.GetNames(typeof(SoundType));

                for (int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.SetParent(sound.transform);
                }

                audioSources[(int)SoundType.BGM].loop = true;
            }
        }
    }

    private void Start()
    {
        Play("b_normal_normal_1", SoundType.BGM);
        // test
        //Add("e_backTracking_backTracking_1");
        //Add("e_backTrackingEnd_backTrackingEnd_1");
    }

    private void Update()
    {
        if (effect_clips.Count > 0 && !audioSources[(int)SoundType.EFFECT].isPlaying)
            Play(effect_clips.Dequeue(), SoundType.EFFECT);
    }

    public void StopAllSound()
    {
        foreach (var item in audioSources)
        {
            item.clip = null;
            item.Stop();
        }

        audioClips.Clear();
    }

    public void Play(AudioClip audioClip, SoundType soundType = SoundType.EFFECT)
    {
        if (!audioClip)
            return;

        AudioSource audioSource;

        if (soundType == SoundType.BGM)
        {
            audioSource = audioSources[(int)SoundType.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource = audioSources[(int)SoundType.EFFECT];
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, SoundType soundType = SoundType.EFFECT) => Play(GetOrAddAudioClip(path, soundType), soundType);

    public void Add(string path, SoundType soundType = SoundType.EFFECT) => effect_clips.Enqueue(GetOrAddAudioClip(path, soundType));

    AudioClip GetOrAddAudioClip(string path, SoundType soundType)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (soundType == SoundType.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (!audioClip)
            Debug.LogWarning($"AudioClip Missing!, path info : {path}");

        return audioClip;
    }
}
