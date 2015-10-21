using UnityEngine;
using System.Collections;

[System.Serializable]
public class TreasuryFragmentData {
    public int levelPoints;
    public int gold;
}

public class TreasuryFragment : SaveFragment {

    [SerializeField]
    TreasuryFragmentData data;
    [SerializeField]
    TreasuryFragmentData startingData;

    void Awake() {
        setUpStartingData();
    }

    public int gold {
        get { return data.gold; }
    }
    public int levelPoints {
        get { return data.levelPoints; }
    }

    public void EarnLevelPoints(int amount) {
        data.levelPoints += amount;
        newInfoPresent = true;
    }
    public void SpendLevelPoints(int amount) {
        if (amount > data.levelPoints) {
            Debug.LogError("ERROR TREASURY: tried to spend more poitns that allowed. must check first");
            return;
        }
        data.levelPoints -= amount;
        newInfoPresent = true;
    }

    public void EarnGold(int amount) {
        data.gold += amount;
        newInfoPresent = true;
    }
    public void SpendGold(int amount) {
        if (amount > data.gold) {
            Debug.LogError("ERROR TREASURY: tried to spend more gold that allowed. must check first");
            return;
        }
        data.gold -= amount;
        newInfoPresent = true;
    }

    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            data = graph as TreasuryFragmentData;
        }
    }

    public override void Reset() {
        base.Reset();
        setUpStartingData();
    }

    void setUpStartingData() {
        data.levelPoints = startingData.levelPoints;
        data.gold = startingData.gold;
    }

}
