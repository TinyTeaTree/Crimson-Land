using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Responsible for the rotation of the avatar in the right direction
/// </summary>
public class AvatarLocorotation : MonoBehaviour {

    public Camera cam;

    void Update() {
        Vector3 hitPoint = cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            cam.transform.position.y
        ));

        Vector3 lookat = new Vector3(
            hitPoint.x - transform.position.x,
            0,
            hitPoint.z - transform.position.z
        );
        transform.rotation = Quaternion.LookRotation(lookat);
    }

}
