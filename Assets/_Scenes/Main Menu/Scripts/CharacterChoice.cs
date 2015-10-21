using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterChoice : MonoBehaviour {

    public Button lifeB, speedB, reloadingB, accuracyB;
    public Button lifeM, speedM, reloadingM, accuracyM;
    public Text levelPointsText, xpTillNextLevelText, lifeText, speedText, reloadingText, accuracyText;
    string levelPointsPre, xpTillNextLevelPre, lifePre, speedPre, reloadingPre, accuracyPre;

    CharacterFragment character;
    TreasuryFragment treasury;

    int lifePoints = 0, speedPoints = 0, reloadingPoints = 0, accuracyPoints = 0;

    void Start() {
        character = GlobalController.instance.saveFile.character;
        treasury = GlobalController.instance.saveFile.treasury;

        levelPointsPre = levelPointsText.text;
        xpTillNextLevelPre = xpTillNextLevelText.text;
        lifePre = lifeText.text;
        speedPre = speedText.text;
        reloadingPre = reloadingText.text;
        accuracyPre = accuracyText.text;
    }

    public void SetUp() {
        int levelPoints = GlobalController.instance.saveFile.treasury.levelPoints;
        levelPointsText.text = levelPointsPre + levelPoints.ToString();

        if (levelPoints > 0) {
            lifeB.interactable = true;
            speedB.interactable = true;
            reloadingB.interactable = true;
            accuracyB.interactable = true;
        } else {
            lifeB.interactable = false;
            speedB.interactable = false;
            reloadingB.interactable = false;
            accuracyB.interactable = false;
        }

        lifeM.interactable = lifePoints > 0;
        speedM.interactable = speedPoints > 0;
        reloadingM.interactable = reloadingPoints > 0;
        accuracyM.interactable = accuracyPoints > 0;

        int myXp = character.xp;
        int xpToNextLevel = GlobalController.instance.dataFile.levelData.GetRequiredXp(myXp);
        xpTillNextLevelText.text = xpTillNextLevelPre + xpToNextLevel.ToString();

        lifeText.text = lifePre + character.life;
        speedText.text = speedPre + character.speed.ToString("0.00");
        reloadingText.text = reloadingPre + character.reloading.ToString("0.00");
        accuracyText.text = accuracyPre + character.accuracy.ToString("0.00");
    }


    public void PressedLifeUp() {
        treasury.SpendLevelPoints(1);
        character.EarnLife(1);
        lifePoints++;
        SetUp();
    }
    public void PressedSpeedUp() {
        treasury.SpendLevelPoints(1);
        character.EarnSpeed(0.05f);
        speedPoints++;
        SetUp();
    }
    public void PressedReloadingUp() {
        treasury.SpendLevelPoints(1);
        character.EarnReloading(0.05f);
        reloadingPoints++;
        SetUp();
    }
    public void PressedAccuracyUp() {
        treasury.SpendLevelPoints(1);
        character.EarnAccuracy(0.05f);
        accuracyPoints++;
        SetUp();
    }

    public void PressedLifeDown() {
        treasury.EarnLevelPoints(1);
        character.SpendLife(1);
        lifePoints--;
        SetUp();
    }
    public void PressedSpeedDown() {
        treasury.EarnLevelPoints(1);
        character.SpendSpeed(0.05f);
        speedPoints--;
        SetUp();
    }
    public void PressedReloadingDown() {
        treasury.EarnLevelPoints(1);
        character.SpendReloading(0.05f);
        reloadingPoints--;
        SetUp();
    }
    public void PressedAccuracyDown() {
        treasury.EarnLevelPoints(1);
        character.SpendAccuracy(0.05f);
        accuracyPoints--;
        SetUp();
    }

}
