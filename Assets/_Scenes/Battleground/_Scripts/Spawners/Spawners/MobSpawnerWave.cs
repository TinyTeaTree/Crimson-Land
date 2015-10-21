using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnInfo2 {
    public EnemyBehaviour enemyPrefab;
    public int amount;
}

public class MobSpawnerWave : MobSpawner {

    public List<EnemySpawnInfo2> enemies;

    public override void StartSpawningCycle() {
        for (int i=0; i<enemies.Count; ++i) {
            for (int j=0; j<enemies[i].amount; ++j) {
                spawn(enemies[i].enemyPrefab);
            }
        }
    }

    void spawn(EnemyBehaviour enemy) {
        float x = Random.Range(leftBoundry.position.x, rightBoundry.position.x);
        float z = Random.Range(lowerBoundry.position.z, upperBoundry.position.z);
        spawnMob(enemy, x, z);
    }

    public override void StopSpawningCycle() {
        base.StopSpawningCycle();
    }
}
