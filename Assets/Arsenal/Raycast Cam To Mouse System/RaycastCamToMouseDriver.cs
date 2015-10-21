using UnityEngine;
using System.Collections;

namespace Arsenal {

    /// <summary>
    /// This driver is responsible for sending raycast objects into the scene originating 
    /// from the camera near clipping plane
    /// </summary>
    public class RaycastCamToMouseDriver : MonoBehaviour {

        public Camera myRaycastingCamera;

        /// <summary>
        /// Camera Near clipping plane ----> Mouse Screen Coordinates
        /// </summary>
        public void ShootCameraToMouseRay() {
            if (myRaycastingCamera != null) {
                RaycastHit hit;
                Ray ray = myRaycastingCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) == true) {
                    ICaptureCamToMouseRaycast capturer = hit.collider.gameObject.GetComponent(typeof(ICaptureCamToMouseRaycast)) as ICaptureCamToMouseRaycast;

                    if (capturer != null) {
                        capturer.CaptureRaycast();
                    } else {
                        Debug.LogWarning("RaycastDriver hit something without ICaptureRaycast");
                    }

                }
            } else {
                Debug.LogError("Error: No raycasting camera was references");
            }
        }

    }

}



