using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobSpawnerBoarderSprinkle : MobSpawner {

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
