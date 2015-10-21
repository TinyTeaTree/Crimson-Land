using UnityEngine;
using System.Collections;

[System.Serializable]
public class BattleInfo {
    public TerrainHolder terrain;
    public MobSpawnerManager mobSpawner;
}

public class BattlesData : MonoBehaviour {

    public BattleInfo[] battles;

}
