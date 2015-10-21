using UnityEngine;
using System.Collections;

/// <summary>
/// This predict algorithm predicts by how much time it will take for the mob to reach the player
/// </summary>
public class EnemyMovePredict : EnemyMove {

    Vector3 prevPos;

    void Update() {
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
        if (target == null) {
            Debug.LogError("Why target is null? " + name);
            return Vector3.zero;
        }
        float distance = (target.position - transform.position).magnitude;
        float time = distance / speed;

        Vector3 targetDeltaDistance = (target.position - prevPos);
        Vector3 fullDelta = targetDeltaDistance * (time / Time.deltaTime);

        return target.position + fullDelta;
    }

}
