using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Arsenal {

    [RequireComponent(typeof(Camera))]
    public class CameraAspectScript : MonoBehaviour {
        [SerializeField]
        float cameraAspect;

        void Start() {
            GetComponent<Camera>().aspect = cameraAspect;
        }
    }

}