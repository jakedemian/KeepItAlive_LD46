using System;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour {
    public Sprite on;
    public Sprite off;

    private Image myImage;

    private void Start() {
        myImage = GetComponent<Image>();
        SetSprite();
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(AudioManager.Instance.ToggleMusic);
    }

    private void Update() {
        SetSprite();
    }

    private void SetSprite() {
        Sprite spriteToUse = AudioManager.Instance.musicPlaying ? on : off;
        //Debug.Log("setting to " + spriteToUse.name);
        myImage.sprite = spriteToUse;
    }
}