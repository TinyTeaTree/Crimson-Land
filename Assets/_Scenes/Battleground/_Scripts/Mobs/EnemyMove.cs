using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour, IEnemyMove {

    protected Transform target = null;

    protected static float minApproachDistance = 0.05f;
    public float speed;

    protected bool isMoving = false;

    virtual public void SetUp(Transform target) {
        this.target = target;
        isMoving = true;
    }

    virtual public void PerformAttack(float attackDelay) {
        StopAllCoroutines();
        StartCoroutine(delayMovementCo(attackDelay));
    }

    IEnumerator delayMovementCo(float attackDelay) {
        isMoving = false;
        yield return new WaitForSeconds(attackDelay);
        isMoving = true;
    }

}
