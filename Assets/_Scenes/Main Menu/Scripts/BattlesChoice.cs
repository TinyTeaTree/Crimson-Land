using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattlesChoice : MonoBehaviour {

    public Button[] battles;

    BattlesFragment fragment;

    void Start() {
        fragment = GlobalController.instance.saveFile.battles;
    }

    public void SetUp() {
        for (int i=0; i<battles.Length; ++i) {
            if (fragment.data.battles.Count > i) {
                battles[i].interactable = fragment.data.battles[i].isUnlocked;
            } else {
                battles[i].interactable = false;
            }
        }
    }

}
