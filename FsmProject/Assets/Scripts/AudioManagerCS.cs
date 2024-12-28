using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCS : MonoBehaviour
{
    public static AudioManagerCS instance;
    public Sound[] sounds;

    // Master volume control
    [Range(0f, 1f)]
    public float masterVolume = 1f;

    // Mute toggle variables
    private bool musicMutetoggle;
    private bool sfxMutetoggle;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * masterVolume; // Use master volume
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.volume = s.volume * masterVolume; // Apply master volume
        s.source.Play();
    }

    public void PlayWithRandomPitch(string name, float minPitch, float maxPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        float originalPitch = s.source.pitch; // Save the original pitch
        s.source.pitch = UnityEngine.Random.Range(minPitch, maxPitch); // Randomize pitch within the specified range
        s.source.volume = s.volume * masterVolume; // Apply master volume
        s.source.Play();
        s.source.pitch = originalPitch; // Reset to original pitch after playing
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.Pause();
    }

    public void unPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.UnPause();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        s.source.volume = s.volume * masterVolume; // Apply master volume
        s.source.PlayOneShot(s.clip);
    }

    public void MuteSfx()
    {
        sfxMutetoggle = !sfxMutetoggle;
        toggleTag(Sound.SoundTag.sfx, sfxMutetoggle);
        PlayerPrefs.SetInt("sfxVolume", sfxMutetoggle ? 0 : 1);
    }

    public void MuteMusic()
    {
        musicMutetoggle = !musicMutetoggle;
        toggleTag(Sound.SoundTag.music, musicMutetoggle);
        PlayerPrefs.SetInt("musicVolume", musicMutetoggle ? 0 : 1);
    }

    public void MuteAll_Ad()
    {
        // Implementation for muting all sounds for ads
        SetMasterVolume(0);
    }

    public void UnMuteAll_Ad()
    {
        // Implementation for unmuting all sounds after ads
        SetMasterVolume(1);
    }

    public List<Sound> GetSounds0fTag(Sound.SoundTag tag)
    {
        var returnList = new List<Sound>();

        foreach (Sound s in sounds)
        {
            if (s.CompareTag(tag))
                returnList.Add(s);
        }
        return returnList;
    }

    public void toggleTag(Sound.SoundTag tag, bool mute)
    {
        foreach (var item in GetSounds0fTag(tag))
        {
            item.mute(mute);
        }
    }

    // Method to adjust master volume
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;

        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * masterVolume; // Update volume for each sound
        }

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
    }
}
