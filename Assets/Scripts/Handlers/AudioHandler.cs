﻿using UnityEngine;
using UnityEngine.Serialization;

public class AudioHandler : MonoBehaviour
{
    #region Audio Variables
    [Header("Audio Variables")]
    [SerializeField] private FloatReference musicVolume;
    [SerializeField] private FloatReference sfxVolume;
    [FormerlySerializedAs("musicPlayer")]
    [SerializeField] private AudioSource bgmAudioSource;
    [FormerlySerializedAs("sfxPlayer")]
    [SerializeField] private AudioSource sfxAudioSource;
    #endregion

    private void Start()
    {
        bgmAudioSource.volume = musicVolume.Value;
        sfxAudioSource.volume = sfxVolume.Value;
    }

    public void MusicVolumeRefreshed()
    {
        bgmAudioSource.volume = musicVolume.Value;
    }

    public void SFXVolumeRefreshed()
    {
        sfxAudioSource.volume = sfxVolume.Value;
    }

    public void PlayAudio(Audio targetAudio)
    {
        AudioSource source = null;
        switch (targetAudio.type)
        {
            case AudioType.BGM:
                source = bgmAudioSource;
                break;
            case AudioType.SFX:
                source = sfxAudioSource;
                break;
            default:
            case AudioType.None:
                return;
        }

        source.clip = targetAudio.clip;
        source.loop = targetAudio.canLoop;
        source.Play();
    }
}
