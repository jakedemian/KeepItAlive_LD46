using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public SoundGroup[] soundGroups;

    public GameObject musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public float musicVolume;
    private bool gameIsStillBeingPlayed = true;


    public bool musicPlaying = true;
    private Image musicButtonImage;
    
    
    public static AudioManager Instance;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (SoundGroup sg in soundGroups) {
            sg.source = gameObject.AddComponent<AudioSource>();
            foreach (Sound s in sg.sounds) {
                s.source = sg.source;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    }

    private void Start() {
        GetComponent<AudioSource>().volume = musicVolume;
        if (musicButtonImage == null) {
            SetMusicButtonImg();
        }
    }

    private void SetMusicButtonImg() {
        musicButtonImage = musicButton.GetComponent<Image>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            ToggleMusic();
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Tried to play sound " + name + " which was not found in AudioManager's sounds array.");
            return;
        }

        s.source.Play();
    }

    public void PlayFromSoundGroup(string name) {
        SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == name);
        if (sg == null) {
            Debug.LogWarning("Tried to play sound from sound group " + name +
                             " which was not found in AudioManager's sounds array.");
            return;
        }

        int randomIndex = Random.Range(0, sg.sounds.Length);

        sg.sounds[randomIndex].source.clip = sg.sounds[randomIndex].clip;
        sg.sounds[randomIndex].source.Play();
    }

    public void ToggleMusic() {
        musicPlaying = !musicPlaying;
        float volume = musicPlaying ? musicVolume : 0f;
        GetComponent<AudioSource>().volume = volume;
        Debug.Log("music toggled " + (musicPlaying ? "on" : "off"));
    }

    // public void SetMusicUISprite() {
    //     if (musicButtonImage == null) {
    //         SetMusicButtonImg();
    //     }
    //     
    //     Sprite img = musicPlaying ? musicOnSprite : musicOffSprite;
    //     musicButtonImage.sprite = img;
    //
    //     float volume = musicPlaying ? musicVolume : 0f;
    //     GetComponent<AudioSource>().volume = volume;
    // }

    private IEnumerator ManageMusicButtonState() {
        while (gameIsStillBeingPlayed) {
            if (musicButton != null && musicButtonImage == null) {
                SetMusicButtonImg();
            }
            
            //SetMusicUISprite();
            yield return new WaitForSeconds(0.2f);
        }
    }
}