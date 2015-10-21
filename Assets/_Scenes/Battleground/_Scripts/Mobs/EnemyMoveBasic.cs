using UnityEngine;
using System.Collections;

public class EnemyMoveBasic : EnemyMove {

    void Update() {
        if (isMoving == true) {            
            Vector3 direction = target.position - transform.position;
            if (direction.magnitude > minApproachDistance) {
                Vector3 velocity = direction.normalized * speed * Time.deltaTime;
                velocity.y = 0;
                transform.Translate(velocity, Space.World);
                transform.SetY(0);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

}
