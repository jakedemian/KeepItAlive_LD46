using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public float pregameCountdownTime = 1f;
    
    public static GameController Instance;
    void Awake() {
        Instance = this;
    }

    void Start() {
        Countdown.Instance.gameObject.SetActive(true);
        StartCoroutine(nameof(StartCountdown));
    }

    private IEnumerator StartCountdown() {
        float timer = 0f;
        Countdown.Instance.Activate(timer, pregameCountdownTime);
        while (timer < pregameCountdownTime) {
            timer += Time.deltaTime;
            Countdown.Instance.timer = timer;
            yield return null;
        }
        Countdown.Instance.gameObject.SetActive(false);
        BallController.Instance.gameStarted = true;
    }

    public void Restart(){
        Application.LoadLevel(Application.loadedLevel);
    }
}