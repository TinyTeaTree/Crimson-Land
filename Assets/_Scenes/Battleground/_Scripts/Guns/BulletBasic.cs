using UnityEngine;
using System.Collections;

public class BulletBasic : Bullet {

    public float speed = 12f;
    public float distance = 30f;
	
	// Update is called once per frame

    float distancePassed = 0;

	void Update () {
        Vector3 translation = Vector3.forward * speed * Time.deltaTime;

        distancePassed += translation.magnitude;
        if (distancePassed > distance) {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

}
