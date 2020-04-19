using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject endgameMenu;
    public GameObject startMenu;
    public GameObject blobPrefab;
    public GameObject tutorialPanel;

    public static bool gameHasStarted = false;
    public static bool isFirstPlay = true;
    
    public float pregameCountdownTime;
    public bool paused;
    
    private float deathWaitTime = 1.5f;
    
    public static GameController Instance;
    void Awake() {
        Instance = this;
    }

    void Start() {
        //AudioManager.Instance.SetMusicUISprite();

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
        float countDownTime = isFirstPlay ? 5f : pregameCountdownTime;
        Countdown.Instance.Activate(timer, countDownTime);
        while (timer < countDownTime) {
            timer += Time.deltaTime;
            Countdown.Instance.timer = timer;
            yield return null;
        }
        Countdown.Instance.gameObject.SetActive(false);
        BallController.Instance.gameStarted = true;
    }

    public void EndGame() {
        paused = true;

        // spawn slime chunks 
        int numBlobsToSpawn = 3;
        Vector2 ballPos = BallController.Instance.transform.position;
        Vector2 ballVel = BallController.Instance.velocity;
        for (int i = 0; i < numBlobsToSpawn; i++) {
            GameObject blob = Instantiate(blobPrefab, ballPos, Quaternion.identity);
            float xVelVariation = Random.Range(-4f, 4f);
            float yVelVariation = Random.Range(0f, 1f);
            Vector2 blobVelocity = ballVel + new Vector2(xVelVariation, yVelVariation);
            blob.GetComponent<DeathBlob>().Init(blobVelocity);
        }
        Destroy(BallController.Instance.gameObject);
        
        StartCoroutine(nameof(EndGameWait));
    }

    private IEnumerator EndGameWait() {
        float timer = 0f;
        while (timer < deathWaitTime) {
            timer += Time.deltaTime;
            yield return null;
        }
        
        endgameMenu.SetActive(true);
        int score = Score.Instance.GetScore();
        bool isHighScore = score > StaticScoreTracker.GetHighScore();
        StaticScoreTracker.SubmitNewScore(score);
        endgameMenu.GetComponent<EndgameMenu>().Init(score, isHighScore);
    }

    public void Restart() {
        isFirstPlay = false;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void StartGame() {
        AudioManager.Instance.PlayFromSoundGroup("squish");
        paused = false;
        startMenu.SetActive(false);
        tutorialPanel.SetActive(true);
        gameHasStarted = true;
        
        Countdown.Instance.gameObject.SetActive(true);
        StartCoroutine(nameof(StartCountdown));
    }
}