using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Behaviour: the script can lerp the scale of an UI element activated by hover.
/// 
/// Condition: the script calculates its original scale during start
/// </summary>
public class UIHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Vector3 targetScale;
    public float lerpCoefficient;
    public float minDistance;

    Vector3 starterScale;

    bool isScalingUp = false, isScalingDown = false;

    int scaling = 0;

    void Start() {
        starterScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (isScalingUp == false) {
            StartCoroutine(scaleUp());
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isScalingDown == false) {
            StartCoroutine(scaleDown());
        }
    }

    IEnumerator scaleUp() {
        scaling = 1;
        isScalingUp = true;
        float distance = (transform.localScale - targetScale).magnitude;

        while (distance > minDistance) {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                targetScale,
                lerpCoefficient * Time.unscaledDeltaTime
            );

            yield return null;
            if (scaling != 1) {
                isScalingUp = false;
                yield break;
            }

            distance = (transform.localScale - targetScale).magnitude;
        }

        isScalingUp = false;
        scaling = 0;
    }

    IEnumerator scaleDown() {
        scaling = -1;
        isScalingDown = true;
        float distance = (transform.localScale - starterScale).magnitude;

        while (distance > minDistance) {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                starterScale,
                lerpCoefficient * Time.unscaledDeltaTime
            );

            yield return null;
            if (scaling != -1) {
                isScalingDown = false;
                yield break;
            }

            distance = (transform.localScale - starterScale).magnitude;
        }

        isScalingDown = false;
        scaling = 0;
    }

}