using UnityEngine;
using System.Collections;

public class GunMachine : Gun {

    public float shootingRate;

    float timer;

    public override void PressedShoot() {
        StartCoroutine(shootCo());
    }

    public override void UnpressedShoot() {
        StopAllCoroutines();
    }

    IEnumerator shootCo() {

        float timeToShoot = 60.0f / shootingRate;
        timer = timeToShoot;

        while (true) {

            while (timer < timeToShoot) {
                timer += Time.deltaTime;
                yield return null;
            }

            spawnBullet();
            timer = 0;

        }
    }

}
