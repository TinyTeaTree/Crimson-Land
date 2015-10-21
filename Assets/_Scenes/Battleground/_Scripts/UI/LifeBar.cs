using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeBar : MonoBehaviour {

    public Text lifeText;
    public RectTransform fill;
    public RectTransform fillFullSize;
    public RectTransform fillMinSize;

    string premadeText;

    int maxLife;

    float width;

    void Awake() {
        premadeText = lifeText.text;
    }

    void Start() {      
        width = fillFullSize.rect.width - fillMinSize.rect.width;
    }

    public void SetPropetries(int maxLife, int life) {
        this.maxLife = maxLife;
        SetLife(life);
    }

    public void SetLife(int life) {
        width = fillFullSize.rect.width - fillMinSize.rect.width;
        lifeText.text = premadeText + life.ToString();
        fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillMinSize.rect.width +  width * (float)life / (float)maxLife);
    }

}
