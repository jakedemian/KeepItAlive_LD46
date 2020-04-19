using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
    public float paddleSpeedFactor;
    public float rotationLerpSpeed = 2f;
    public float targetAngle;
    public float maxAngle;
    public float targetAngleFactor = 2f;
    public GameObject paddleSlimeTexture;
    public float slimeTextureVerticalOffset = 0.5f;

    private Vector3 currentAngle;
    public EdgeCollider2D collider;

    public static PaddleController Instance;
    private float targetAngle2;

    void Awake() {
        Instance = this;
    }


    void Start() {
        currentAngle = transform.eulerAngles;
        collider = GetComponent<EdgeCollider2D>();
    }

    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector2(mousePos.x, transform.position.y);
        Vector2 nextPosition = Vector2.Lerp(transform.position, targetPosition, paddleSpeedFactor * Time.deltaTime);

        DoPaddleRotation(nextPosition);
        MoveTowardsMouse(nextPosition);
    }

    private void MoveTowardsMouse(Vector2 nextPosition) {
        transform.position = nextPosition;
    }

    private void DoPaddleRotation(Vector2 nextPosition) {
        float distance = Vector2.Distance(transform.position, nextPosition);
        float direction = nextPosition.x < transform.position.x ? 1f : -1f;

        if (distance < 0.05f) {
            targetAngle2 = 0f;
        }

        targetAngle2 = distance * direction * targetAngleFactor;
        //Debug.Log(targetAngle2);

        currentAngle = new Vector3(0, 0,
            Mathf.Clamp(
                Mathf.LerpAngle(currentAngle.z, targetAngle2, Time.deltaTime * rotationLerpSpeed),
                -maxAngle, maxAngle));

        transform.eulerAngles = currentAngle;
    }

    public void MakeSlimeTexture(RaycastHit2D hit) {
        // TODO
        
        // GameObject slimeTexture = Instantiate(paddleSlimeTexture);
        // slimeTexture.transform.parent = transform;
        //
        // // move it to the correct spot
        // Vector2 texPosition = slimeTexture.transform.position;
        // float correctY = transform.position.y + (hit.normal * slimeTextureVerticalOffset).y;
        // slimeTexture.transform.position = new Vector2(texPosition.x, correctY);
    }

    public EdgeCollider2D GetCollider() {
        return collider;
    }
}