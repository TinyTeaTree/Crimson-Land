using UnityEngine;
using System.Collections;

/// <summary>
/// Just a proxy to hold data information, maybe will be used later to upgrade to a global file
/// </summary>
public class DataFile : MonoBehaviour {

    [HideInInspector]
    public LevelData levelData;
    [HideInInspector]
    public GunsData gunsData;
    [HideInInspector]
    public BattlesData battlesData;

    void Awake() {
        levelData = GetComponentInChildren<LevelData>();
        gunsData = GetComponentInChildren<GunsData>();
        battlesData = GetComponentInChildren<BattlesData>();
    }

}
