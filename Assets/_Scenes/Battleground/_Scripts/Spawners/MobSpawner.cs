using UnityEngine;
using System.Collections;

public abstract class MobSpawner : MonoBehaviour, ITerrainRequester {

    MobSpawnerManager manager;

    public float startTime;
    public float duration;

    protected Transform upperBoundry, lowerBoundry, leftBoundry, rightBoundry;
    bool isSet = false;

    bool _isSpent = false;
    public bool isSpent {
        get { return _isSpent; }
        set { _isSpent = value; }
    }

    public void SetProperties(MobSpawnerManager manager) {
        this.manager = manager;
    }

    public void InnerStartSpawningCycle() {
        if (isSet == true) {
            StartSpawningCycle();
        } else {
            Debug.LogError("ERROR RACE: trying to start mob spawning before setting terrain");
        }
    }

    public abstract void StartSpawningCycle();

    public virtual void StopSpawningCycle(){
        isSpent = true;   
    }

    protected void spawnMob(EnemyBehaviour enemy, float x, float z) {
        EnemyBehaviour newEnemy = Instantiate(enemy);
        manager.AddMobReference(newEnemy);
        newEnemy.transform.position = new Vector3(
            x,
            0,
            z
        );
    }

    public void SetTerrain(TerrainHolder terrain) {        
        upperBoundry = terrain.upBoundry;
        lowerBoundry = terrain.downBoundry;
        rightBoundry = terrain.rightBoundry;
        leftBoundry = terrain.leftBoundry;
        isSet = true;
    }
}
