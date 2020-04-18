using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounceAnimation : MonoBehaviour {
    #region Singleton
    public static BallBounceAnimation Instance;
    void Awake() {
        Instance = this;
    }
    #endregion

    public bool isAnimating = false;
    public float animationTime = 1f;
    public float animationSpeed = 1f;
    public float animationStrength = 1f;
    public float animationDampening = 1f;
    
    public void DoBallBounce() {
        StartCoroutine(nameof(DoAnimation));
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