using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {
    public float paddleSpeedFactor;
    public bool reverseScrollWheel = false;
    public float targetAngle;
    public float maxAngle;

    private Vector3 currentAngle;
    public EdgeCollider2D collider;

    public static PaddleController Instance;

    void Awake() {
        Instance = this;
    }


    void Start() {
        currentAngle = transform.eulerAngles;
        collider = GetComponent<EdgeCollider2D>();
    }

    void Update() {
        bool mouseLeft = Input.GetMouseButton(0);
        bool mouseRight = Input.GetMouseButton(1);

        if (mouseLeft ^ mouseRight) {
            DoPaddleRotation(mouseLeft);
        }

        //MoveTowardsMouse();
    }

    private void MoveTowardsMouse() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector2(mousePos.x, transform.position.y);

        transform.position = Vector2.Lerp(transform.position, targetPosition, paddleSpeedFactor * Time.deltaTime);
    }

    private void DoPaddleRotation(bool left) {
        int direction = left ? 1 : -1;

        currentAngle = new Vector3(0, 0,
            Mathf.Clamp(
                Mathf.LerpAngle(currentAngle.z, targetAngle * direction, Time.deltaTime),
                -maxAngle, maxAngle));

        transform.eulerAngles = currentAngle;
    }

    public EdgeCollider2D GetCollider() {
        return collider;
    }
}