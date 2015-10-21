using UnityEngine;
using System.Collections;

/// <summary>
/// Placment: Battlground Controller must be on parent object
/// </summary>
public class PauseManager : MonoBehaviour {

    public float pauseTime = 1f;

    BattlegroundController controller;
    [HideInInspector]
    public bool isPause = false;
    [HideInInspector]
    public bool isInMiddle = false;

    void Start() {
        controller = transform.parent.GetComponent<BattlegroundController>();
    }

    public void Pause() {
        if (isPause == false && isInMiddle == false) {
            isPause = true;
            StartCoroutine(pauseCo());
            controller.uiManager.pauseUI.Show();
        }
    }

    public void Unpause() {
        if (isPause == true && isInMiddle == false) {
            isPause = false;
            StartCoroutine(unpauseCo());
            controller.uiManager.pauseUI.Hide();
        }
    }

    IEnumerator pauseCo() {
        float timePassed = 0;
        isInMiddle = true;
        while (timePassed < pauseTime) {
            float ratio = 1 - timePassed / pauseTime;

            Time.timeScale = ratio;

            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }
        isInMiddle = false;
        Time.timeScale = 0;
    }

    IEnumerator unpauseCo() {
        float timePassed = 0;
        isInMiddle = true;
        while (timePassed < pauseTime) {
            float ratio = timePassed / pauseTime;

            Time.timeScale = ratio;

            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }
        isInMiddle = false;
        Time.timeScale = 1;
    }

}
