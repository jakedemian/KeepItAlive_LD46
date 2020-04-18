using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BallController : MonoBehaviour {
    public LayerMask paddle;
    public float gravity;
    public float maxSpeed;
    public float bounceSpeed;
    public float criticalDampeningY;
    public float criticalDampeningFactor;

    private Vector2 velocity = Vector2.zero;
    private RaycastHit2D hit;

    private bool isCooldownTimer = false;
    private float collisionCooldownTime = 0.25f;
    private float collisionCooldownTimer = 0f;
    private CircleCollider2D collider;

    private bool isRising = false;
    private float maxYReached = 0f;


    public static BallController Instance;

    void Awake() {
        Instance = this;
        Application.targetFrameRate = 60;
    }
    
    
    
    void Start() {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update() {
        if (isRising) {
            if (velocity.y < 0f) {
                isRising = false;
                Debug.Log(maxYReached);
            }
            else {
                maxYReached = transform.position.y;
            }
        }
        
        if (!CheckCollisions()) {
            ApplyGravity(); // dont do gravity on the collision frame
        }
        else {
            isRising = true;
        }
        
        DoMove();
    }

    private void ApplyGravity() {
        float newYSpeed;
        if (transform.position.y > criticalDampeningY && velocity.y > 0f) {
            // critically dampen the velocity
            newYSpeed = velocity.y - (criticalDampeningFactor * gravity * Time.deltaTime);
        }
        else {
            newYSpeed = velocity.y - (gravity * Time.deltaTime);
        }

        velocity = new Vector2(velocity.x, newYSpeed);

    }

    private bool CheckCollisions() {
        float halfHeight = collider.radius;
        if (transform.position.y - halfHeight < -3f) {
            // "collision"
            transform.position = new Vector2(transform.position.x, -3 + halfHeight);
            CheckRaycast();
            return true;
        }

        return false;
    }

    private bool CheckRaycast() {
        Vector2 towardsPaddle = PaddleController.Instance.transform.position - transform.position;
        hit = Physics2D.Raycast(transform.position, towardsPaddle.normalized, Mathf.Infinity, paddle);
        if (hit.collider != null) {
            velocity = hit.normal * bounceSpeed;
            Debug.Log(velocity.y);
            BallBounceAnimation.Instance.DoBallBounce();
            AudioManager.Instance.PlayFromSoundGroup("squish");
            return true;
        }

        return false;
    }

    private void DoMove() {
        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}