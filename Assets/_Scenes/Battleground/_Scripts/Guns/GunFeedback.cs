using UnityEngine;
using System.Collections;

/// <summary>
/// Place this script on the "Gun Anchor" that is ment to hold the gun,
/// The gun anchor must be facing forwards at the time of the Start of this script
/// to record the proper position
/// </summary>
public class GunFeedback : MonoBehaviour {

    public float lerpCoefficient;
    public float maxBack;
    Vector3 originalPosition;
    //Quaternion originalRotation;

    void Start() {
        originalPosition = transform.localPosition;
        //originalRotation = transform.localRotation;
    }

    float amountBack = 0;

    public void PushBack(float force) {
        if (force + amountBack > maxBack) {
            force = maxBack - amountBack;
        }

        amountBack += force;
        transform.Translate(Vector3.forward * -force, Space.Self);
    }

    void Update() {
        amountBack = Mathf.Lerp(amountBack, 0, lerpCoefficient * Time.deltaTime);
        transform.localPosition = originalPosition;
        transform.Translate(Vector3.forward * -amountBack, Space.Self);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, lerpCoefficient * Time.deltaTime);
    }

}
