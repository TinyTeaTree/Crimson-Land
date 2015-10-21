using UnityEngine;
using System.Collections;

public class AvatarCollision : MonoBehaviour {

    AvatarBehaviour avatar;

    void Awake() {
        avatar = GetComponent<AvatarBehaviour>();
    }

    void OnTriggerEnter(Collider other) {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy.isAttackCooldown == false) {
            avatar.GetHit(enemy);
        }
    }

}
