using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] List<Sound> sounds;
    [SerializeField] AudioSource musicSounds, sfxSounds;
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Start() {
        PlayMusic("Main Theme");
    }

    public void PlayMusic(string soundName) {
        Sound sound = sounds.Find(s => s.name == soundName);

        if(sound != null) {
            musicSounds.clip = sound.audioClip;
            musicSounds.Play();
        }
    }

    public void PlaySfx(string soundName) {
        Sound sound = sounds.Find(s => s.name == soundName);

        if(sound != null) {
            sfxSounds.PlayOneShot(sound.audioClip);
        }
    }

    public void Mute() {
        musicSounds.mute = true;
    }

    public void Unmute() {
        musicSounds.mute = false;
    }
}
