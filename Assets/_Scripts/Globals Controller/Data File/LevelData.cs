using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelInfo {
    [Tooltip("Start counting at 1")]
    public int level;
    [Tooltip("How much xp is required to gain at this level in order to level up to the next level")]
    public int xp;
    [Tooltip("How many level points are rewarded for leveling up from this level")]
    public int levelPoints;
    [Tooltip("How much gold is rewarded for leveling up from this level")]
    public int goldReward;
}

public class LevelData : MonoBehaviour {

    public LevelInfo[] levels;

    public int GetLevel(int xp) {
        int level = 1;

        while (xp > 0) {
            if (level-1 >= levels.Length) {
                return level;
            }

            if (levels[level-1].xp <= xp) {
                xp -= levels[level-1].xp;
                level++;
            } else {
                break;
            }
        }

        return level;
    }

    public float GetLevelPercent(int xp) {
        float percent = 0;

        int level = 1;
        while (xp > 0) {
            if (level-1 >= levels.Length) {
                return level;
            }

            if (levels[level-1].xp <= xp) {
                xp -= levels[level-1].xp;
                level++;
            } else {
                percent = (float)xp / (float)levels[level-1].xp;
                break;
            }
        }

        return percent;
    }

    public int GetLevelXP(int xp) {
        return levels[GetLevel(xp)-1].xp;
    }

    /// <summary>
    /// How much xp you have on the current level that you are
    /// </summary>
    public int GetOverflowXP(int xp) {
        int level = 1;

        while (xp > 0) {
            if (level-1 >= levels.Length) {
                return 0;
            }

            if (levels[level-1].xp <= xp) {
                xp -= levels[level-1].xp;
                level++;
            } else {
                break;
            }
        }

        return xp;
    }

    /// <summary>
    /// How much xp you need to get to next level
    /// </summary>
    public int GetRequiredXp(int xp) {
        int level = 1;

        while (xp > 0) {
            if (level-1 >= levels.Length) {
                return 0;
            }

            if (levels[level-1].xp <= xp) {
                xp -= levels[level-1].xp;
                level++;
            } else {
                break;
            }
        }

        return levels[level-1].xp - xp;
    }

#if UNITY_EDITOR
    void Update() {
        checkLevelsProperly();
    }

    void checkLevelsProperly() {
        if (levels == null || levels.Length < 1) {
            Debug.LogError("ERROR LEVEL DATA: no level data found");
            return;
        }

        for (int i=0; i<levels.Length; ++i) {
            if (levels[i].level != i+1) {
                Debug.LogError("ERROR LEVEL DATA: level " + (i+1).ToString() + " Has instead filled in level " + levels[i].ToString());
                return;
            }
        }
    }
#endif

}