using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarEffect : MonoBehaviour {

    public Text lifeText;
    public Image fillImage, outlineImage;

    public Color damagedFillColor, damagedOutlineColor, damagedTextColor;
    public float damagedSize;
    public float damagedEffectTime, damagedBackEffectTime;

    float val = 0;

    int maxLife;

    Vector4 fillDelta, outlineDelta, textDelta;
    Color originalFillColor, originalOutlineColor, originalTextColor;

    void Start() {
        originalFillColor = fillImage.color;
        originalOutlineColor = outlineImage.color;
        originalTextColor = lifeText.color;

        fillDelta = new Vector4(
            damagedFillColor.r - fillImage.color.r,
            damagedFillColor.g - fillImage.color.g,
            damagedFillColor.b - fillImage.color.b,
            damagedFillColor.a - fillImage.color.a
        );
        outlineDelta = new Vector4(
            damagedOutlineColor.r - outlineImage.color.r,
            damagedOutlineColor.g - outlineImage.color.g,
            damagedOutlineColor.b - outlineImage.color.b,
            damagedOutlineColor.a - outlineImage.color.a
        );
        textDelta = new Vector4(
            damagedTextColor.r - lifeText.color.r,
            damagedTextColor.g - lifeText.color.g,
            damagedTextColor.b - lifeText.color.b,
            damagedTextColor.a - lifeText.color.a
        );
    }

    public void Effect() {
        StopAllCoroutines();
        StartCoroutine(effectCo());
    }
    IEnumerator effectCo() {
        float timePassed = val * damagedEffectTime;

        while (timePassed < damagedEffectTime) {

            float ratio = timePassed / damagedEffectTime;

            fillImage.color = new Color(
                originalFillColor.r + fillDelta.x * ratio,
                originalFillColor.g + fillDelta.y * ratio,
                originalFillColor.b + fillDelta.z * ratio,
                originalFillColor.a + fillDelta.w * ratio
            );
            outlineImage.color = new Color(
                originalOutlineColor.r + outlineDelta.x * ratio,
                originalOutlineColor.g + outlineDelta.y * ratio,
                originalOutlineColor.b + outlineDelta.z * ratio,
                originalOutlineColor.a + outlineDelta.w * ratio
            );
            lifeText.color = new Color(
                originalTextColor.r + textDelta.x * ratio,
                originalTextColor.g + textDelta.y * ratio,
                originalTextColor.b + textDelta.z * ratio,
                originalTextColor.a + textDelta.w * ratio
            );

            transform.localScale = Vector3.one * (1 + damagedSize * ratio);

            val = ratio;

            timePassed += Time.deltaTime;
            yield return null;
        }

        fillImage.color = new Color(
            originalFillColor.r + fillDelta.x,
            originalFillColor.g + fillDelta.y,
            originalFillColor.b + fillDelta.z,
            originalFillColor.a + fillDelta.w
        );
        outlineImage.color = new Color(
            originalOutlineColor.r + outlineDelta.x,
            originalOutlineColor.g + outlineDelta.y,
            originalOutlineColor.b + outlineDelta.z,
            originalOutlineColor.a + outlineDelta.w
        );
        lifeText.color = new Color(
            originalTextColor.r + textDelta.x,
            originalTextColor.g + textDelta.y,
            originalTextColor.b + textDelta.z,
            originalTextColor.a + textDelta.w
        );

        transform.localScale = Vector3.one * (1 + damagedSize);

        val = 1;

        timePassed = 0;

        while (timePassed < damagedBackEffectTime) {
            float ratio = 1 - timePassed / damagedBackEffectTime;

            fillImage.color = new Color(
                originalFillColor.r + fillDelta.x * ratio,
                originalFillColor.g + fillDelta.y * ratio,
                originalFillColor.b + fillDelta.z * ratio,
                originalFillColor.a + fillDelta.w * ratio
            );
            outlineImage.color = new Color(
                originalOutlineColor.r + outlineDelta.x * ratio,
                originalOutlineColor.g + outlineDelta.y * ratio,
                originalOutlineColor.b + outlineDelta.z * ratio,
                originalOutlineColor.a + outlineDelta.w * ratio
            );
            lifeText.color = new Color(
                originalTextColor.r + textDelta.x * ratio,
                originalTextColor.g + textDelta.y * ratio,
                originalTextColor.b + textDelta.z * ratio,
                originalTextColor.a + textDelta.w * ratio
            );

            transform.localScale = Vector3.one * (1 + damagedSize * ratio);

            val = ratio;


            timePassed += Time.deltaTime;
            yield return null;
        }

        fillImage.color = new Color(
            originalFillColor.r,
            originalFillColor.g,
            originalFillColor.b,
            originalFillColor.a
        );
        outlineImage.color = new Color(
            originalOutlineColor.r,
            originalOutlineColor.g,
            originalOutlineColor.b,
            originalOutlineColor.a
        );
        lifeText.color = new Color(
            originalTextColor.r,
            originalTextColor.g,
            originalTextColor.b,
            originalTextColor.a
        );

        transform.localScale = Vector3.one;

        val = 0;
    }

}
