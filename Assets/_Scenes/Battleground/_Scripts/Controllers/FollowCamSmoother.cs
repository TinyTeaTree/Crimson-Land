using UnityEngine;
using System.Collections;

public class FollowCamSmoother : MonoBehaviour {

    public float lerpCoefficiant;

    public AvatarBehaviour avatar;
    public Transform camSmoother;

    void LateUpdate() {
        transform.position = Vector3.LerpUnclamped(
            transform.position,
            camSmoother.position,
            lerpCoefficiant * Time.deltaTime
        );

        //transform.LookAt(avatar.transform, Vector3.forward);
    }


}
