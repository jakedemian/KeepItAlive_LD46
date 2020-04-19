using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Wall : MonoBehaviour {
    public float moveSpeed = 1f;
    
    private bool hasScored = false;
    void Update() {
        if (GameController.Instance.paused) return;

        if (transform.position.x < -15f) {
            Destroy(gameObject);
            return;
        }
        
        if (!hasScored && BallController.Instance.transform.position.x > transform.position.x) {
            hasScored = true;
            Score.Instance.IncrementScore();
        }
        
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
