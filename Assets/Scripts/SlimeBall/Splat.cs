using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour {
    public float minSpeed ;
    public float maxSpeed;
    public float lifeSpan;

    private Vector2 targetPosition;
    private float speed;
    private float timer;
    void Start() {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > lifeSpan) {
            Destroy(gameObject);    
        }

        float percent = 1 - (timer / lifeSpan);
        transform.localScale = new Vector2(percent, percent);
        
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    public void Init(Vector2 target) {
        targetPosition = target;
    }
}