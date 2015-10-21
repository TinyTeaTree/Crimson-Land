using UnityEngine;
using System.Collections;


/// <summary>
/// This script attaches the position of an object to another object like the transform heirarchy does.
/// 
/// *Note that this script uses the LateUpdate to catche the current changes in positioning.
/// </summary>
public class AttachPosition : MonoBehaviour {

    public Transform attachTo;

    Vector3 delta = Vector3.zero;

	// Use this for initialization
	void Start () {
        delta = transform.position - attachTo.position;
	}

    void LateUpdate() {
        transform.position = attachTo.position + delta;
    }
	

}
