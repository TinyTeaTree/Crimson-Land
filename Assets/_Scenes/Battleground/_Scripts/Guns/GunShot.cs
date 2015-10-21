using UnityEngine;
using System.Collections;

public class GunShot : Gun {

    public float averageBulletsPerRound;
    public float bulletsGap;
    public float roundRate;
    public float angleRadius;
    [Tooltip("this is the area around the the bullet spawn point that is allowed to be spawned at, in essence we add a random offset of X and Y ranged by this number to the spawning position")]
    public float offsetRadius;

    float minBulletCount, maxBulletCount;

    bool isCharging = false;
    bool isPressed = false;

    void Start() {
        minBulletCount = averageBulletsPerRound - bulletsGap;
        maxBulletCount = averageBulletsPerRound + bulletsGap;
    }

    public override void PressedShoot() {
        isPressed = true;
        if (isCharging == false) {
            StartCoroutine(shootCo());
        }
    }

    public override void UnpressedShoot() {
        isPressed = false;
    }

    IEnumerator shootCo() {
        isCharging = true;

        while (isPressed == true) {
            spawnManyBullets();
            yield return new WaitForSeconds(60.0f / roundRate);
        }

        isCharging = false;
    }

    void spawnManyBullets() {
        int bulletsToSpawn = Mathf.RoundToInt(Random.Range(minBulletCount, maxBulletCount));

        for (int i=0; i<bulletsToSpawn; ++i) {
            spawnBullet();
        }
    }

    protected override GameObject spawnBullet() {
        GameObject bullet = base.spawnBullet();
        float angleToShoot = Random.Range(-angleRadius, angleRadius);
        bullet.transform.Translate(Random.Range(-offsetRadius, offsetRadius), 0, Random.Range(-offsetRadius, offsetRadius), Space.World);
        bullet.transform.Rotate(0, angleToShoot, 0);
        return bullet;
    }


}
