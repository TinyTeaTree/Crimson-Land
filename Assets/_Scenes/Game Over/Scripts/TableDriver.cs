using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TableDriver : MonoBehaviour {

    public RectTransform table;
    public RectTransform minSize;

    public GameObject topScoreButton, creditsButton, backButton;

    public GameObject topScoreTable, creditsTable;

    public Text title;
    public string topScoreTitle, creditsTitle;

    public float expantionTime;

    bool isExpanded = false;

    RectTransform myRect;

    void Awake() {
        myRect = transform as RectTransform;
    }

    public void PressedTopScores() {
        topScoreButton.SetActive(false);
        creditsButton.SetActive(false);
        topScoreTable.SetActive(true);
        creditsTable.SetActive(false);
        title.text = topScoreTitle;
        Expand();
    }

    public void PressedCredits() {
        topScoreButton.SetActive(false);
        creditsButton.SetActive(false);
        topScoreTable.SetActive(false);
        creditsTable.SetActive(true);
        title.text = creditsTitle;
        Expand();
    }

    public void PressedBack() {
        topScoreTable.SetActive(false);
        creditsTable.SetActive(false);
        title.text = "";
        Retract();
    }

    public void Expand() {
        if (isExpanded == false) {
            StartCoroutine(expancCo());
        }
    }

    public void Retract() {
        if (isExpanded == true) {
            StartCoroutine(retractCo());
        }
    }

    IEnumerator retractCo() {
        float timePassed = 0;
        backButton.SetActive(false);
        while (timePassed < expantionTime) {
            float ratio = 1 - timePassed / expantionTime;

            float size = minSize.rect.height + ratio * (myRect.rect.height - minSize.rect.height);

            table.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);


            timePassed += Time.deltaTime;
            yield return null;
        }

        table.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minSize.rect.height);
        topScoreButton.SetActive(true);
        creditsButton.SetActive(true);
        isExpanded = false;
    }

    IEnumerator expancCo() {
        float timePassed = 0;

        while (timePassed < expantionTime) {
            float ratio = timePassed / expantionTime;

            float size = minSize.rect.height + ratio * (myRect.rect.height - minSize.rect.height);

            table.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);


            timePassed += Time.deltaTime;
            yield return null;
        }

        table.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, myRect.rect.height);
        backButton.SetActive(true);
        isExpanded = true;
    }

}
