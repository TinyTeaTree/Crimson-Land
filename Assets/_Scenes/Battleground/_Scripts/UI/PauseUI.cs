using UnityEngine;
using System.Collections;

public class PauseUI : MonoBehaviour {

    public float showTime;
    public float offY = 100;
    public float onY = -10;

    RectTransform myRect;

    bool isShown = false;
    bool isInMiddle = false;

    void Start() {
        myRect = transform as RectTransform;
        myRect.anchoredPosition = new Vector2(myRect.position.x, offY);
        gameObject.SetActive(false);
    }

    public void Show() {
        if (isShown == false && isInMiddle == false) {
            gameObject.SetActive(true);
            StartCoroutine(showCo());
            isShown = true;
        }
    }

    public void Hide() {
        if (isShown == true && isInMiddle == false) {
            StartCoroutine(hideCo());
            isShown = false;
        }
    }

    IEnumerator showCo() {
        float timePassed = 0;
        isInMiddle = true;       
        while (timePassed < showTime) {

            float ratio = timePassed / showTime;

            myRect.anchoredPosition = new Vector2(myRect.position.x, offY + ratio * (onY - offY));

            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }
        isInMiddle = false;
    }

    IEnumerator hideCo() {
        float timePassed = 0;
        isInMiddle = true;
        while (timePassed < showTime) {

            float ratio = 1 - timePassed / showTime;

            myRect.anchoredPosition = new Vector2(myRect.position.x, offY + ratio * (onY - offY));

            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        isInMiddle = false;
    }

}
