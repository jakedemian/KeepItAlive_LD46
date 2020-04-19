using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject endgameMenu;
    public GameObject startMenu;

    public static bool gameHasStarted = false;
    
    public float pregameCountdownTime = 1f;
    public bool paused;
    
    public static GameController Instance;
    void Awake() {
        Instance = this;
    }

    void Start() {
        Debug.Log(gameHasStarted);
        if (!gameHasStarted) {
            startMenu.SetActive(true);
            paused = true;
        }
        else {
            paused = false;
            Countdown.Instance.gameObject.SetActive(true);
            StartCoroutine(nameof(StartCountdown));
        }
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

    public void EndGame() {
        paused = true;
        endgameMenu.SetActive(true);
        int score = Score.Instance.GetScore();
        bool isHighScore = score > StaticScoreTracker.GetHighScore();
        StaticScoreTracker.SubmitNewScore(score);
        endgameMenu.GetComponent<EndgameMenu>().Init(score, isHighScore);
    }

    public void Restart(){
        Application.LoadLevel(Application.loadedLevel);
    }

    public void StartGame() {
        AudioManager.Instance.PlayFromSoundGroup("squish");
        paused = false;
        startMenu.SetActive(false);
        gameHasStarted = true;
        
        Countdown.Instance.gameObject.SetActive(true);
        StartCoroutine(nameof(StartCountdown));
    }
}