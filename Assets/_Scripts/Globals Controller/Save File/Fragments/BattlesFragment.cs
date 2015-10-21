using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BattleFragmentInfo {
    public bool isUnlocked;

    public BattleFragmentInfo(bool isUnlocked) {
        this.isUnlocked = isUnlocked;
    }
}

[System.Serializable]
public class BattlesFragmentData {
    public List<BattleFragmentInfo> battles;
}

public class BattlesFragment : SaveFragment {

    public BattlesFragmentData data;

    void Awake() {
        setUpStartingData();
    }

    public void UnlockNextBattle() {
        data.battles.Add(new BattleFragmentInfo(true));
    }

    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            data = graph as BattlesFragmentData;
        }
    }

    public override void Reset() {
        base.Reset();

        setUpStartingData();
    }

    void setUpStartingData() {
        data.battles.Clear();
        data.battles.Add(new BattleFragmentInfo(true));
    }
}
