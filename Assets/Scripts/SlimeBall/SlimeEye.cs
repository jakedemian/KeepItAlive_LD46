using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEye : MonoBehaviour {
    public Transform slimeBall;
    public Transform slimePupil;
    public Vector2 relativeOrigin;
    public float followSpeed;
    public float pupilPlacementFactor;
    public float pupilMaxDistance;
    public float pupilHorizOffset;

    private Vector2 targetLocation;
    void Start() {
        targetLocation = (Vector2) slimeBall.position + relativeOrigin;
        transform.position = targetLocation; // hard set it on start
    }

    void Update() {
        if (GameController.Instance.paused) return;
        
        targetLocation = (Vector2) slimeBall.position + relativeOrigin;
        transform.position = Vector3.Lerp(transform.position, targetLocation, followSpeed * Time.deltaTime); 
        
        //update pupil
        float slimeBallVelocity = BallController.Instance.velocity.y;
        float pupilY = Mathf.Clamp(slimeBallVelocity * pupilPlacementFactor, -pupilMaxDistance, pupilMaxDistance);
        slimePupil.position = (Vector2) transform.position + new Vector2(pupilHorizOffset, pupilY);
    }
}