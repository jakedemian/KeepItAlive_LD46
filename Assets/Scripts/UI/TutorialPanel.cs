using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour {
    public float fadeStartTime = 3f;
    public float fadeTime = 1f;

    public GameObject mouseImg;
    public GameObject text;

    private float timer = 0f;

    void Update() {
        timer += Time.deltaTime;

        if (timer > fadeStartTime) {
            //StartCoroutine(nameof(Fade));
            gameObject.SetActive(false);
        }
    }

    IEnumerator Fade() {
        float timer = 0f;
        while (timer < fadeTime) {
            float alpha = 1f - (timer / fadeTime);
            Image imgComponent = GetComponent<Image>();
            Image mouseImgComponent = mouseImg.GetComponent<Image>();
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();

            imgComponent.color = GetNewAlpha(imgComponent.color, alpha);
            mouseImgComponent.color = GetNewAlpha(mouseImgComponent.color, alpha);
            textComponent.color = GetNewAlpha(textComponent.color, alpha);
            
            yield return null;
        }
        gameObject.SetActive(false);
    }

    Color GetNewAlpha(Color c, float a) {
        return new Color(c.r, c.g, c.b, a);
    }
}
