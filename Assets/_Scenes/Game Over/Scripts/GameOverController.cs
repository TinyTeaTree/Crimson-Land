using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {

    public TableElement original;

    public Text placmentText;

    Transform contentHolder;

    void Awake() {
        contentHolder = original.transform.parent;
    }
    

	// Use this for initialization
	void Start () {
        placmentText.text = "";
        StartCoroutine(waitForScoreFragmentCo());
	}

    public void PressedExit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PressedBackMenu() {
        GlobalController.instance.transitionery.GoTo(TagsLocations.loginMenu);
    }

    IEnumerator waitForScoreFragmentCo() {
        while (GlobalController.instance.systemFile.achievementFragment.isInitialized == false) {
            yield return null;
        }
        while (GlobalController.instance.saveFile.profile.isInitialized == false) {
            yield return null;
        }

        //if (string.IsNullOrEmpty(GlobalController.instance.saveFile.profileFragment.username) == false &&
        //    GlobalController.instance.scoreElement != null &&
        //    string.IsNullOrEmpty(GlobalController.instance.scoreElement.username) == false) {

        //    placmentText.text = "# " + getPlace(GlobalController.instance.scoreElement.score).ToString();

        //    GlobalController.instance.saveFile.achievementFragment.AddScore(
        //        GlobalController.instance.scoreElement.username,
        //        GlobalController.instance.scoreElement.score
        //    );

        //    GlobalController.instance.saveFile.profileFragment.username = "";
        //    GlobalController.instance.scoreElement = null;
        //}

        ScoreElement[] scores = GlobalController.instance.systemFile.achievementFragment.data.scoreTable.ToArray();


        for (int i=0; i<scores.Length; ++i) {
            TableElement newElement = Instantiate(original);
            newElement.gameObject.SetActive(true);
            
            newElement.transform.SetParent(contentHolder);
            newElement.transform.ResetTransform();
            newElement.SetProperties(scores[i].username, scores[i].score, i+1);
        }
    }

    int getPlace(int points) {
        int place = 1;

        ScoreElement[] scores = GlobalController.instance.systemFile.achievementFragment.data.scoreTable.ToArray();

        for (int i=0; i<scores.Length; ++i) {
            if (scores[i].score >= points) {
                place++;
            }
        }

        return place;
    }

}
