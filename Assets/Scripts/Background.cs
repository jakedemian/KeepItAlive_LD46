using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    private float moveSpeed;
    void Update() {
        transform.Translate(moveSpeed * Vector2.left);
    }

    public void SetSpeed(float speed) {
        moveSpeed = speed;
    }
}