using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    private int score = 0;
    private TextMeshProUGUI scoreUI;

    public static Score Instance;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        scoreUI = GetComponent<TextMeshProUGUI>();
        scoreUI.text = score.ToString();
    }

    public void IncrementScore() {
        score++;
        scoreUI.text = score.ToString();
    }

    public int GetScore() {
        return score;
    }
}
