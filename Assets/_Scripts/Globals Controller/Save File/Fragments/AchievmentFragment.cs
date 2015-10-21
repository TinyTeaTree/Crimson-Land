using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreElement {
    public string username;
    public int score;

    public ScoreElement(string username, int score) {
        this.username = username;
        this.score = score;
    }
}

[System.Serializable]
public class AchievmentFragmentData {
    public List<ScoreElement> scoreTable;
}

public class AchievmentFragment : SaveFragment {

    public AchievmentFragmentData data;

    public void AddScore(string username, int score) {
        if (data.scoreTable == null) {
            data.scoreTable = new List<ScoreElement>();
        }

        bool added = false;
        for (int i=data.scoreTable.Count-1; i>= 0; --i) {
            if (data.scoreTable[i].score >= score) {
                data.scoreTable.Insert(i+1, new ScoreElement(username, score));
                added = true;
                break;
            }
        }

        if (added == false) {
            data.scoreTable.Insert(0, new ScoreElement(username, score));
        }
        newInfoPresent = true;
    }

    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            AchievmentFragmentData frag = graph as AchievmentFragmentData;

            this.data = frag;
        }
    }


    public override void Reset() {
        base.Reset();
        data.scoreTable = new List<ScoreElement>();
    }
}