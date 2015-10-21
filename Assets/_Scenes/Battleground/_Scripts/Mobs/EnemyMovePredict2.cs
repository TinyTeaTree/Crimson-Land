using UnityEngine;
using System.Collections;

public class EnemyMovePredict2 : EnemyMove {

    [Tooltip("How much into the future should the predict algorith work")]
    public float averagePredictTime;
    public float predictTimeGap;

    float predictTime;

    Vector3 prevPos;

    void Start() {
        predictTime = Random.Range(averagePredictTime - predictTimeGap, averagePredictTime + predictTimeGap);
    }

    void LateUpdate() {
        if (isMoving == true) {
            Vector3 direction = getPredictedPosition() - transform.position;
            if (direction.magnitude > minApproachDistance) {
                Vector3 velocity = direction.normalized * speed * Time.deltaTime;
                velocity.y = 0;
                transform.Translate(velocity, Space.World);
                transform.SetY(0);
                transform.rotation = Quaternion.LookRotation(direction);
            }
            prevPos = target.position;
        }
    }

    Vector3 getPredictedPosition() {
        Vector3 targetDeltaDistance = (target.position - prevPos);
        Vector3 fullDelta = targetDeltaDistance * (predictTime / Time.deltaTime);

        return target.position + fullDelta;
    }
}
