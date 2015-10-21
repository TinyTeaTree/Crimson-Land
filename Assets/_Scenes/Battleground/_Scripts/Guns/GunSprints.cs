using UnityEngine;
using System.Collections;

public class GunSprints : Gun {

    public int roundsPerSprint;
    public float roundsPerMinute, sprintsPerMinutes;

    float timer = 0;

    void Update() {
        timer += Time.deltaTime;
    }

    public override void PressedShoot() {
        StartCoroutine(shootCo());
    }

    public override void UnpressedShoot() {
        StopAllCoroutines();
    }

    IEnumerator shootCo() {

        float timeToShoot = 60.0f / sprintsPerMinutes;

        while (true) {

            while (timer < timeToShoot) {
                yield return null;
            }

            StartCoroutine(sprint());
            timer = 0;

        }
    }

    IEnumerator sprint() {
        float timeToShoot = 60.0f / roundsPerMinute;

        int shootCounter = 0;

        while (shootCounter < roundsPerSprint) {

            shootCounter++;
            spawnBullet();

            yield return new WaitForSeconds(timeToShoot);
        }
    }

}
