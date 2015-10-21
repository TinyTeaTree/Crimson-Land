using UnityEngine;
using System.Collections;

public class GunSpray : GunMachine {

    public float offsetAngle;

    protected override GameObject spawnBullet() {
        GameObject bullet = base.spawnBullet();
        bullet.transform.Rotate(0, Random.Range(-offsetAngle, offsetAngle), 0);
        return bullet;
    }

}