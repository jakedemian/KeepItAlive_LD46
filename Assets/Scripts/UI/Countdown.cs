using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour {
    private TextMeshProUGUI timerText;

    public static Countdown Instance;
    
    [HideInInspector] public float timer = 0f;
    private float totalTime;

    void Awake() {
        Instance = this;
    }
    
    void Start() {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    public void Activate(float timer, float totalTime) {
        this.totalTime = totalTime;
        this.timer = timer;
    }

    void LateUpdate() {
        float displayTime = totalTime - timer;
        timerText.text = displayTime.ToString("F1");
    }
}
