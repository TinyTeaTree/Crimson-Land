using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

    EnemyBehaviour me;

    void Awake() {
        me = GetComponent<EnemyBehaviour>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.Bullet) {
            me.GetHit(other.GetComponent<Bullet>());
            Destroy(other.gameObject);
        } else if (other.tag == Tags.Avatar) {
            me.PerformAttack();
        }
    }

}
