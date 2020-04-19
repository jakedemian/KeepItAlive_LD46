using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlob : MonoBehaviour {
    public float animationTime = 1f;
    public float animationSpeed = 1f;
    public float animationStrength = 1f;
    public float animationDampening = 1f;
    public float lifeSpan;
    
    [Range(0f,1f)]
    public float minSize;
    
    [Range(0f,1f)]
    public float maxSize;
    
    private Vector2 velocity;
    private float timer = 0f;
    void Start() {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 359f));
        float scale = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(scale, scale);
        
        GetComponent<Rigidbody2D>().velocity = velocity;
        //StartCoroutine(nameof(DoAnimation));
    }

    void Update()
    {
        if (transform.position.y < -10f) {
            Destroy(gameObject);
        }
        
        timer += Time.deltaTime;
        if (timer > lifeSpan) {
            Destroy(gameObject);    
        }

        float percent = 1 - (timer / lifeSpan);
        transform.localScale = new Vector2(percent, percent);
        
        //transform.Translate(velocity * Time.deltaTime);
    }

    public void Init(Vector2 v) {
        velocity = v;
    }
    
    private IEnumerator DoAnimation() {
        float timer = 0f;
        while (timer < animationTime) {
            float newYScale = EvaluateSine(timer);
            if (!float.IsNaN(newYScale)) {
                transform.localScale = new Vector3(1, newYScale, 1);
            }

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(1,1,1);
    }
    
    private float EvaluateSine(float timer) {
        var a = animationSpeed;
        var b2 = animationStrength * animationStrength;
        var c = animationDampening;
        return ((b2 * Mathf.Sin(a * timer)) / (c * timer)) + 1f;
    }
}
