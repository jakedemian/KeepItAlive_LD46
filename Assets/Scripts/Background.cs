using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    private float moveSpeed;
    void Update() {
        if (GameController.Instance.paused) return;
        
        if (transform.position.x < -21f) {
            Destroy(gameObject);
            return;
        }
        
        transform.Translate(moveSpeed * Vector2.left);
    }

    public void SetSpeed(float speed) {
        moveSpeed = speed;
    }
}