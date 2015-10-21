using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Behaviour: the script can lerp the color of an Image element activated by hover.
/// 
/// Condition: Image or Text or anything that inherits from the Graphic class my be a component
/// Condiiton: The color you want to be when no hover has to be present during the Start()
/// </summary>
public class UIHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public float lerpCoefficient;
    public float minDistance;
    public Color onHoverColor;

    Graphic basicGraphic;
    Color fromColor;

    int transition = 0;
    bool isColorUp = false, isColorDown = false;

    float distance = 0;

    void Start() {
        basicGraphic = GetComponent<Graphic>();
        fromColor = basicGraphic.color;
    }


    public void OnPointerEnter(PointerEventData eventData) {
        if (isColorUp == false) {
            StartCoroutine(colorUp());
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isColorDown == false) {
            StartCoroutine(colorDown());
        }
    }

    IEnumerator colorUp() {
        transition = 1;
        isColorUp = true;

        while (distance < 1 - minDistance) {
            basicGraphic.color = new Color(
                Mathf.Lerp(fromColor.r, onHoverColor.r, distance),
                Mathf.Lerp(fromColor.g, onHoverColor.g, distance),
                Mathf.Lerp(fromColor.b, onHoverColor.b, distance),
                Mathf.Lerp(fromColor.a, onHoverColor.a, distance)
            );

            yield return null;
            if (transition != 1) {
                isColorUp = false;
                yield break;
            }

            distance = Mathf.Lerp(distance, 1f, lerpCoefficient * Time.deltaTime);
        }

        basicGraphic.color = onHoverColor;
        isColorUp = false;
        transition = 0;
    }

    IEnumerator colorDown() {
        transition = -1;
        isColorDown = true;

        while (distance > minDistance) {
            basicGraphic.color = new Color(
                Mathf.Lerp(fromColor.r, onHoverColor.r, distance),
                Mathf.Lerp(fromColor.g, onHoverColor.g, distance),
                Mathf.Lerp(fromColor.b, onHoverColor.b, distance),
                Mathf.Lerp(fromColor.a, onHoverColor.a, distance)
            );

            yield return null;
            if (transition != -1) {
                isColorDown = false;
                yield break;
            }

            distance = Mathf.Lerp(distance, 0f, lerpCoefficient * Time.deltaTime);
        }

        basicGraphic.color = fromColor;
        isColorDown = false;
        transition = 0;
    }
}
