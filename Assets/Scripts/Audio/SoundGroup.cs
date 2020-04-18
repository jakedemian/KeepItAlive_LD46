using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundGroup {
    public Sound[] sounds;
    public string name;

    [HideInInspector]
    public AudioSource source;
}