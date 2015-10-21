using UnityEngine;
using System.Collections;



public static class TransformExtensions {

	public static void SetX(this Transform transform, float x){
        Vector3 newPos = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
    public static void SetY(this Transform transform, float y) {
        Vector3 newPos = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = newPos;
    }
    public static void SetZ(this Transform transform, float z) {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, z);
        transform.position = newPos;
    }
    public static void SetPos(this Transform transform, float x, float y, float z) {
        Vector3 newPos = new Vector3(x, y, z);
        transform.position = newPos;
    }
    public static void SetPos(this Transform transform, Vector3 pos) {
        transform.position = pos;
    }
    public static void SetTransform(this Transform transform, Transform otherTransform) {
        transform.position = otherTransform.position;
        transform.rotation = otherTransform.rotation;
    }

    public static void ResetTransform(this Transform transform) {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void LerpPosition(this Transform transform, Transform otherTransform, float ratio) {
        transform.position = Vector3.Lerp(transform.position, otherTransform.position, ratio);
    }
    public static void LerpRotation(this Transform transform, Transform otherTransform, float ratio) {
        transform.rotation = Quaternion.Lerp(transform.rotation, otherTransform.rotation, ratio);
    }
    public static void LerpTransform(this Transform transform, Transform otherTransform, float ratio) {
        transform.position = Vector3.Lerp(transform.position, otherTransform.position, ratio);
        transform.rotation = Quaternion.Lerp(transform.rotation, otherTransform.rotation, ratio);
    }
	
}



