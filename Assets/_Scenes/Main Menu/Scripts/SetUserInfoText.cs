using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetUserInfoText : MonoBehaviour {

    public Text nameText, levelText, xpText, goldText;

    string namePre, levelPre, xpPre, goldPre;

    void Start() {
        namePre = nameText.text;
        levelPre = levelText.text;
        xpPre = xpText.text;
        goldPre = goldText.text;

        StartCoroutine(waitForSaveFile());
    }

    IEnumerator waitForSaveFile() {
        while (GlobalController.instance.saveFile.isInitialized == false) {
            yield return null;
        }

        nameText.text = namePre + GlobalController.instance.saveFile.profile.id;
        int level = GlobalController.instance.dataFile.levelData.GetLevel(GlobalController.instance.saveFile.character.xp);
        levelText.text = levelPre + level.ToString();
        xpText.text = xpPre + GlobalController.instance.saveFile.character.xp;
        goldText.text = goldPre + GlobalController.instance.saveFile.treasury.gold;
    }

}
