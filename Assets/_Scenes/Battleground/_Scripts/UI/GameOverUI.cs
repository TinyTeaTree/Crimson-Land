using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour {

    public float showTime;
    Image myImage;

    void Awake() {
        myImage = GetComponent<Image>();
    }

    public void ShowGameOver() {
        StartCoroutine(showCo());
    }

    IEnumerator showCo() {
        float timePassed = 0;

        while (timePassed < showTime) {

            float ratio = timePassed / showTime;

            myImage.color = new Color(
                myImage.color.r,
                myImage.color.g,
                myImage.color.b,
                ratio
            );

            timePassed += Time.deltaTime;
            yield return null;
        }

        myImage.color = new Color(
            myImage.color.r,
            myImage.color.g,
            myImage.color.b,
            1
        );
    }

}
