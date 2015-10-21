using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnInfo {
    public EnemyBehaviour enemyPrefab;
    public float spawnRatio;
}

public class MobSpawnerRandomSprinkle : MobSpawner {

    public List<EnemySpawnInfo> enemies;

    public float spawnRatePerMinute;

    public float timer = 1;

    float totalSumRatio = 0;

    void Awake() {
        totalSumRatio = 0;
        for (int i=0; i<enemies.Count; ++i) {
            totalSumRatio += enemies[i].spawnRatio;
        }
    }

    IEnumerator spawnUpdate() {
        float time = 0;
        while (time < duration) {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                spawn();
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    void spawn() {
        float x = Random.Range(leftBoundry.position.x, rightBoundry.position.x);
        float z = Random.Range(lowerBoundry.position.z, upperBoundry.position.z);

        spawnMob(getRandomEnemy(), x, z);

        timer = 60.0f / spawnRatePerMinute;
    }

    EnemyBehaviour getRandomEnemy() {
        float randomRoll = Random.Range(0, totalSumRatio);
        float currantStand = 0;
        for (int i=0; i<enemies.Count; ++i) {
            if (randomRoll >= currantStand && randomRoll <= enemies[i].spawnRatio + currantStand) {
                return enemies[i].enemyPrefab;
            }
            currantStand += enemies[i].spawnRatio;
        }

        Debug.LogError("ERROR RANDOM ENEMY: should not reach here if all is set up correctly " + randomRoll);
        return null;
    }

    public override void StartSpawningCycle() {
        StartCoroutine(spawnUpdate());
    }

    public override void StopSpawningCycle() {
        base.StopSpawningCycle();
        StopAllCoroutines();
    }

}
