using UnityEngine;
using System.Collections;

public class CaretHack : MonoBehaviour {

    public Transform properCaretObject;

    void Update() {
        foreach (Transform t in transform) {
            if (t.name == properCaretObject.name && t != properCaretObject) {
                ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)properCaretObject).rect.width);
                ((RectTransform)t).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)properCaretObject).rect.height);
                ((RectTransform)t).anchoredPosition = ((RectTransform)properCaretObject).anchoredPosition;
            }
        }
    }

}
