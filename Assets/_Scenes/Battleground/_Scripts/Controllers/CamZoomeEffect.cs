using UnityEngine;
using System.Collections;

public class CamZoomeEffect : MonoBehaviour {


    public float fov, height, effectTime;
    Camera cam;

    float currentFov, currentHeight;

    void Awake() {
        cam = GetComponent<Camera>();
        
    }

    void Start() {
        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.AVATAR_KILLED, Effect, true, true);
    }

    public void Effect() {
        GetComponent<FollowCamSmoother>().enabled = false;
        StartCoroutine(effectCo());
    }

    IEnumerator effectCo() {
        float timePassed = 0;

        currentFov = cam.fieldOfView;
        currentHeight = cam.transform.position.y;

        while (timePassed < effectTime) {

            float ratio = timePassed / effectTime;

            cam.fieldOfView = currentFov + (ratio * (fov - currentFov));
            cam.transform.SetY(currentHeight + ratio * (height - currentHeight));

            timePassed += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = fov;
        cam.transform.SetY(height);
    }

}
