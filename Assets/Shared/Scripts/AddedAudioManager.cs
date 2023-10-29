using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.Serialization;

public class AddedAudioManager : MonoBehaviour
{
    public static AddedAudioManager instance;
    [SerializeField] private SoundButtonData fxButton;
    [SerializeField] private SoundButtonData musicButton;
    [Space]
    [Range(0, 1)] 
    [SerializeField] private float musicVolume = 0.5f;
    [Range(0, 1)] 
    [SerializeField] private float fxVolume;
    [Range(0.7f, 1)]
    [SerializeField] private float lowPitch = 0.8f;
    [Range(1, 1.3f)]
    [SerializeField] private float highPitch = 1.2f;

    [SerializeField] private AudioClip[] winSounds;
    [SerializeField] private AudioClip[] loseSounds;

    [SerializeField] private AudioClip collected;
    [SerializeField] private AudioClip kick;

    private void Start()
    {
        instance = this;
        
        if (PlayerPrefs.GetFloat("fxVolume") == -1)
			ChangeFxVolume();
        if (PlayerPrefs.GetFloat("MusicVolume") == -1)
            ChangeMusicVolume();
    }

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1, bool steps = false, bool pitch = true)
    {
        if (clip != null)
        {
            GameObject go = new GameObject("SoundFX " + clip.name);
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            float randomPitch = Random.Range(lowPitch, highPitch);
            if (pitch)
                source.pitch = randomPitch;
            source.volume = volume;

            source.Play();
            if (!steps)
                Destroy(go, clip.length);
            return source;
        }

        return null;
    }

    private AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1)
    {
        if (clips != null)
        {
            if (clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if (clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume);
                    return source;
                }
            }
        }

        return null;
    }

    public void PlayWinSound()
    {
        PlayRandom(winSounds, Vector3.zero, fxVolume);
    }
    
    public void PlayLoseSound()
    {
        PlayRandom(loseSounds, Vector3.zero, fxVolume);
    }

    public void PlayCollected()
    {
        PlayClipAtPoint(collected, Vector3.zero, fxVolume);
    }
    
    public void PlayKick()
    {
        PlayClipAtPoint(kick, Vector3.zero, fxVolume);
    }


    public void ChangeMusicVolume()
    {
        musicVolume = musicButton.ChangeVolume(musicVolume);
        MusicPlayer.instance.music.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void ChangeFxVolume()
    {
        fxVolume = fxButton.ChangeVolume(fxVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
    }
}