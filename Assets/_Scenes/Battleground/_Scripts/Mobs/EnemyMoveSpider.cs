using UnityEngine;
using System.Collections;

public class EnemyMoveSpider : EnemyMove {

    public float averageDistance;
    public float distanceGap;
    public float restRatio;

    float minDistance, maxDistance;

    public override void SetUp(Transform target) {
        base.SetUp(target);

        myStart();
    }

    void myStart() {
        minDistance = Mathf.Clamp(averageDistance - distanceGap, 0, 10000);
        maxDistance = Mathf.Clamp(averageDistance + distanceGap, 0, 10000);
        StartCoroutine(moveCo());
    }

    public override void PerformAttack(float attackDelay) {
        StopAllCoroutines();
        StartCoroutine(delay(attackDelay));
    }

    IEnumerator delay(float delay) {
        yield return new WaitForSeconds(delay);
        StartCoroutine(moveCo());
    }

    IEnumerator moveCo() {
        float traversal = Random.Range(minDistance, maxDistance);

        float timeToMove = traversal / speed;

        float timer = 0;

        Vector3 direction = target.position - transform.position;
        Vector3 velocity = direction.normalized * speed;
        transform.rotation = Quaternion.LookRotation(direction);
        velocity.y = 0;

        while (timer < timeToMove) {

            transform.Translate(velocity * Time.deltaTime, Space.World);
            transform.SetY(0);
            

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;
        float timeToWait = timeToMove * restRatio;
        while (timer < timeToWait) {

            transform.rotation = Quaternion.LookRotation(target.position - transform.position);

            timer += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(moveCo());
    }
    



}
