using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterFragmentData {
    public int maxHealth;
    public float speed;
    public float reloading;
    public float accuracy;
    public int xp;
}

public class CharacterFragment : SaveFragment {

    [SerializeField]
    CharacterFragmentData data;
    [SerializeField]
    CharacterFragmentData startingData;

    LevelData levelData;
    TreasuryFragment treasury;

    public int life { get { return data.maxHealth; } }
    public float speed { get { return data.speed; } }
    public int xp { get { return data.xp; } }
    public float reloading { get { return data.reloading; } }
    public float accuracy { get { return data.accuracy; } }

    void Awake() {
        setUpStartingData();
    }

    void Start() {
        levelData = GlobalController.instance.dataFile.levelData;
        treasury = GlobalController.instance.saveFile.treasury;
    }

    public void EarnXP(int amount) {
        int currentLevel = levelData.GetLevel(data.xp);
        data.xp += amount;
        newInfoPresent = true;

        int levelDifference = levelData.GetLevel(data.xp) - currentLevel;

        if (levelDifference > 0) {
            int levelPointsReward = levelData.levels[currentLevel-1].levelPoints;
            int goldPointsReward = levelData.levels[currentLevel-1].goldReward;
            treasury.EarnLevelPoints(levelPointsReward);
            treasury.EarnGold(goldPointsReward);
        }
    }
    public void EarnLife(int amount) {
        data.maxHealth += amount;
        newInfoPresent = true;
    }
    public void SpendLife(int amount) {
        if (data.maxHealth < amount) {
            Debug.LogError("ERROR AMOUNT: cannot reduce more health then present, must check first");
            return;
        }

        data.maxHealth -= amount;
        newInfoPresent = true;
    }
    public void EarnSpeed(float amount) {
        data.speed += amount;
        newInfoPresent = true;
    }
    public void SpendSpeed(float amount) {
        if (data.speed < amount) {
            Debug.LogError("ERROR AMOUNT: cannot reduce more speed then present, must check first");
            return;
        }

        data.speed -= amount;
        newInfoPresent = true;
    }
    public void EarnReloading(float amount) {
        data.reloading += amount;
        newInfoPresent = true;
    }
    public void SpendReloading(float amount) {
        if (data.reloading < amount) {
            Debug.LogError("ERROR AMOUNT: cannot reduce more reloading then present, must check first");
            return;
        }

        data.reloading -= amount;
        newInfoPresent = true;
    }
    public void EarnAccuracy(float amount) {
        data.accuracy += amount;
    }
    public void SpendAccuracy(float amount) {
        if (data.accuracy < amount) {
            Debug.LogError("ERROR AMOUNT: cannot reduce more accuracy then present, must check first");
            return;
        }

        data.accuracy -= amount;
        newInfoPresent = true;
    }


    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            data = graph as CharacterFragmentData;
        }
    }

    public override void Reset() {
        base.Reset();
        setUpStartingData();
    }

    void setUpStartingData() {
        data.maxHealth = startingData.maxHealth;
        data.speed = startingData.speed;
        data.xp = startingData.xp;
    }

    void updateUI() {

    }

}
