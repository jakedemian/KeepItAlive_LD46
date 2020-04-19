using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public GameObject bkndGround;
    public GameObject bkndMountains;

    public float groundMoveSpeed;
    public float mountainMoveSpeed;

    public float groundAdjustment;
    public float mountainAdjustment;

    private GameObject currentGround;
    private GameObject currentMountain;

    private float groundWidth;
    private float mountainWidth;

    void Start() {
        Application.targetFrameRate = 60;
        GameObject g1 = Instantiate(bkndGround);
        g1.GetComponent<Background>().SetSpeed(groundMoveSpeed);
        
        groundWidth = g1.GetComponent<BoxCollider2D>().bounds.size.x;
        currentGround = Instantiate(
            bkndGround, 
            (Vector2) g1.transform.position + (Vector2.right * (groundWidth - groundAdjustment)),
            Quaternion.identity);
        currentGround.GetComponent<Background>().SetSpeed(groundMoveSpeed);

        GameObject m1 = Instantiate(bkndMountains);
        m1.GetComponent<Background>().SetSpeed(mountainMoveSpeed);

        mountainWidth = m1.GetComponent<BoxCollider2D>().bounds.size.x;
        currentMountain = Instantiate(
            bkndMountains, 
            (Vector2) m1.transform.position + (Vector2.right * (mountainWidth - mountainAdjustment)),
            Quaternion.identity);
        currentMountain.GetComponent<Background>().SetSpeed(mountainMoveSpeed);
    }

    void Update() {
        if (currentGround.transform.position.x < 0f) {
            GameObject old = currentGround;
            currentGround = Instantiate(
                bkndGround, 
                (Vector2) currentGround.transform.position + (Vector2.right * (groundWidth - groundAdjustment)),
                Quaternion.identity);
            currentGround.GetComponent<Background>().SetSpeed(groundMoveSpeed);
        }
        
        if (currentMountain.transform.position.x < 0f) {
            currentMountain = Instantiate(
                bkndMountains, 
                (Vector2) currentMountain.transform.position + (Vector2.right * (mountainWidth - mountainAdjustment)),
                Quaternion.identity);
            currentMountain.GetComponent<Background>().SetSpeed(mountainMoveSpeed);
        }
        
        
    }
}