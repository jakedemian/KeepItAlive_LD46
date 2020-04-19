using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallController : MonoBehaviour {
    public LayerMask paddleLayer;
    public LayerMask obstacleLayer;
    public GameObject splatPrefab;

    public float gravity;
    public float maxSpeed;
    public float bounceSpeed;
    public float criticalDampeningY;
    public float criticalDampeningFactor;
    public float requiredHitDistance = 0.5f;
    public int splatMinCount = 3;
    public int splatMaxCount = 5;

    [HideInInspector] public bool gameStarted = false;

    [HideInInspector] public Vector2 velocity = Vector2.zero;

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
        if (!gameStarted || GameController.Instance.paused) return;

        if (transform.position.y < -10) {
            GameController.Instance.EndGame();
            AudioManager.Instance.Play("death");
        }

        CheckObstacleCollision();

        if (!CheckCollisions()) {
            ApplyGravity(); // dont do gravity on the collision frame
        }
        else {
            isRising = true;
        }

        DoMove();
    }

    private void CheckObstacleCollision() {
        Collider2D obstacleCollider = Physics2D.OverlapCircle(transform.position, collider.radius, obstacleLayer);
        if (obstacleCollider) {
            GameController.Instance.EndGame();
            AudioManager.Instance.Play("death");
        }
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
        return CheckRaycast();
    }

    private bool CheckRaycast() {
        if (velocity.y > 0f) return false;

        RaycastHit2D leftHit, rightHit;
        float radius = collider.radius;

        Vector2 botLeft = new Vector2(transform.position.x - radius, transform.position.y - radius);
        Vector2 botRight = new Vector2(transform.position.x + radius, transform.position.y - radius);

        leftHit = Physics2D.Raycast(botLeft, Vector2.down, Mathf.Infinity, paddleLayer);
        rightHit = Physics2D.Raycast(botRight, Vector2.down, Mathf.Infinity, paddleLayer);
        if (leftHit || rightHit) {
            List<float> distances = new List<float>();
            if (leftHit) distances.Add(leftHit.distance);
            if (rightHit) distances.Add(rightHit.distance);

            if (distances.Min() <= requiredHitDistance) {
                var hit = leftHit ? leftHit : rightHit;
                PaddleController.Instance.MakeSlimeTexture(hit);
                velocity = hit.normal * bounceSpeed;
                BallBounceAnimation.Instance.DoBallBounce();
                AudioManager.Instance.PlayFromSoundGroup("squish");
                SpawnSplats();
                return true;
            }
        }

        return false;
    }

    private void SpawnSplats() {
        float direction = 1f;
        int numOfSplats = Random.Range(splatMinCount, splatMaxCount);
        float halfHeight = collider.radius;
        for (int i = 0; i < numOfSplats; i++) {
            GameObject splat = Instantiate(splatPrefab, transform.position - (transform.up * halfHeight),
                Quaternion.identity);
            float xVariation = Random.Range(-0.5f, 0.5f);
            float yVariation = Random.Range(-0.1f, 0.1f);
            Vector2 target = Vector2.right * direction * 2f;
            target = new Vector2(target.x + xVariation, target.y + yVariation);
            Debug.Log(target);
            splat.GetComponent<Splat>().Init(target);
            direction *= -1f; // switch the direction each iteration
        }
    }

    private bool IsHit(RaycastHit2D hit) {
        return hit.collider != null;
    }


    private void DoMove() {
        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}