using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobSpawnerWaveBoarder : MobSpawner {

    public List<EnemySpawnInfo2> enemies;

    public override void StartSpawningCycle() {
        for (int i=0; i<enemies.Count; ++i) {
            for (int j=0; j<enemies[i].amount; ++j) {
                spawn(enemies[i].enemyPrefab);
            }
        }
    }

    void spawn(EnemyBehaviour enemy) {
        int rand = Random.Range(0, 4);
        float val = Random.Range(0.0f, 1.0f);
        float x = 0, z = 0;
        switch (rand) {
            case 0://12 oclock
                z = upperBoundry.position.z + 5;
                x = Mathf.Lerp(leftBoundry.position.x, rightBoundry.position.x, val);
                break;
            case 1:// 3 oclock
                z = Mathf.Lerp(upperBoundry.position.z, lowerBoundry.position.z, val);
                x = rightBoundry.position.x + 5;
                break;
            case 2:// 6 oclock
                z = lowerBoundry.position.z - 5;
                x = Mathf.Lerp(leftBoundry.position.x, rightBoundry.position.x, val);
                break;
            case 3:// 9 oclock
                z = Mathf.Lerp(upperBoundry.position.z, lowerBoundry.position.z, val);
                x = leftBoundry.position.x - 5;
                break;
        }

        spawnMob(enemy, x, z);
    }

    public override void StopSpawningCycle() {
        base.StopSpawningCycle();
    }

}
