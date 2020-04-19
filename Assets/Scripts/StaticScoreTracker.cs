using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticScoreTracker {
    private static List<int> scores = new List<int>();
    private static int highScore = 0;

    public static int GetHighScore() {
        return highScore;
    }

    public static bool SubmitNewScore(int score) {
        scores.Add(score);
        if (score > highScore) {
            highScore = score;
            return true;
        }

        return false;
    }
}
