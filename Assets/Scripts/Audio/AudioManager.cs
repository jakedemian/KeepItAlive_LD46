using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public SoundGroup[] soundGroups;

    public static AudioManager Instance;

    void Awake() {
        Instance = this;

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
}