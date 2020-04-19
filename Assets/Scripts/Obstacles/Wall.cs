using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public float moveSpeed = 1f;
    void Start() {
    }

    void Update() {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
