using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Arsenal {

    /// <summary>
    /// Driver providing the ICaptureRaycast API.
    /// 
    /// ***NOTE*** We implement through interface because the RaycastDriver
    /// Needs to activate a function on the recieving side of the raycast.
    /// </summary>
    public class CaptureRaycastCamToMouseDriver : MonoBehaviour, ICaptureCamToMouseRaycast {

        public UnityEvent onCaptureRaycastEvent;

        public void CaptureRaycast() {
            onCaptureRaycastEvent.Invoke();
        }

    }

}



