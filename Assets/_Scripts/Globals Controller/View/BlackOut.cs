using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackOut : MonoBehaviour {

    public float fadeOutTime, fadeInTime;
    Image image;
    CanvasGroup blocker;

    void Awake() {
        image = GetComponent<Image>();
        blocker = GetComponent<CanvasGroup>();
    }

    public void FadeOutIn() {
        FadeOutIn(fadeOutTime, 0, fadeInTime, null, null);
    }

    public void FadeOutIn(System.Action afterFadeOutAction) {
        FadeOutIn(fadeOutTime, 0, fadeInTime, afterFadeOutAction, null);
    }

    public void FadeOutIn(float timeOut, float timeWait, float timeIn, System.Action afterFadeOutAction, System.Action afterFadeInAction) {       
        StartCoroutine(fadeOutInCo(timeOut, timeWait, timeIn, afterFadeOutAction, afterFadeInAction));
    }

    IEnumerator fadeOutInCo(float timeOut, float timeWait, float timeIn, System.Action afterFadeOutAction, System.Action afterFadeInAction) {
        blocker.blocksRaycasts = true;

        float timePassed = 0;
        while (timePassed < timeOut) {
            float ratio = timePassed / timeOut;
            image.color = new Color(0, 0, 0, ratio);

            timePassed += Time.deltaTime;
            yield return null;
        }

        image.color = Color.black;
        if (afterFadeOutAction != null) {
            afterFadeOutAction();
        }

        if (timeWait > 0) {
            yield return new WaitForSeconds(timeWait);
        }

        timePassed = 0;
        while (timePassed < timeIn) {
            float ratio = 1 - timePassed / timeOut;
            image.color = new Color(0, 0, 0, ratio);

            timePassed += Time.deltaTime;
            yield return null;
        }
        image.color = Color.clear;
        if (afterFadeInAction != null) {
            afterFadeInAction();
        }

        blocker.blocksRaycasts = false;
    }


}
