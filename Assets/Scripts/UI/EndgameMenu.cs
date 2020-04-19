using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndgameMenu : MonoBehaviour {
    public static EndgameMenu Instance;

    private void Awake() {
        Instance = this;
    }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    
    [HideInInspector] public int score;
    [HideInInspector] public bool isHighScore;

    public void Init(int _score, bool _isHighScore) {
        score = _score;
        isHighScore = _isHighScore;
        
        scoreText.text = "You bounced over " + score.ToString() + " arbitrary rock piles!";
        titleText.text = isHighScore ? "New High Score!" : "Whoops!";
    }

    public void Restart() {
        GameController.Instance.Restart();
    }
}