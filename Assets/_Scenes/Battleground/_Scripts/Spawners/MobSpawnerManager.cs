using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MobSpawnerManager : MyMonoBehaviour {

    public List<EnemyBehaviour> currentEnemies = new List<EnemyBehaviour>();

    MobSpawner[] spawners;

    void Awake() {
        spawners = GetComponentsInChildren<MobSpawner>();
        Array.Sort<MobSpawner>(spawners, (ele1, ele2) => {
            if (ele1.startTime > ele2.startTime) {
                return 1;
            } else if (ele1.startTime == ele2.startTime) {
                return 0;
            } else {
                return -1;
            }
        });

        for (int i=0; i<spawners.Length; ++i) {
            spawners[i].SetProperties(this);
        }
    }

    public bool HasKilledEverything() {
        for (int i=0; i<spawners.Length; ++i) {
            if (spawners[i].isSpent == false) {
                return false;
            }
        }
        return currentEnemies.Count == 0;
    }

    public void AddMobReference(EnemyBehaviour enemy) {
        currentEnemies.Add(enemy);
        enemy.SetProperties(this);
    }

    public void RemoveMobReference(EnemyBehaviour enemy) {
        currentEnemies.Remove(enemy);
    }

    public void StartSpawning() {
        StartCoroutine(spawnCo());
    }

    IEnumerator spawnCo() {

        int index = 0;
        float time = 0;

        while (index < spawners.Length) {
            while (time < spawners[index].startTime) {
                time += Time.deltaTime;
                yield return null;
            }

            spawners[index].InnerStartSpawningCycle();
            Invoke(spawners[index].StopSpawningCycle, spawners[index].duration);
            index++;
        }

    }

}
