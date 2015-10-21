using UnityEngine;
using System.Collections;


/// <summary>
/// This script attaches the position of an object to another object like the transform heirarchy does.
/// but clamps to certain boundries
/// 
/// *Note that this script uses the LateUpdate to catche the current changes in positioning.
/// </summary>
public class AttachPositionClamp : MonoBehaviour, ITerrainRequester {

    public Camera cam;

    public Transform attachTo;


    Transform upperBoundry, lowerBoundry, leftBoundry, rightBoundry;

    float maxZ, minZ, maxX, minX;

    Vector3 delta = Vector3.zero;

    bool isSet = false;

    // Use this for initialization
    void Start() {
        delta = transform.position - attachTo.position;
        GlobalController.instance.eventRegistery.AddRegisteration(EEventRegistrationType.SCREEN_RES_CHANGED, calculateBoundries, true, false);
    }

    void LateUpdate() {
        //calculateBoundries();

        transform.position = attachTo.position + delta;

        if (transform.position.x > maxX) {
            transform.SetX(maxX);
        }

        if (transform.position.x < minX) {
            transform.SetX(minX);
        }

        if (transform.position.z > maxZ) {
            transform.SetZ(maxZ);
        }

        if (transform.position.z < minZ) {
            transform.SetZ(minZ);
        }
    }

    public void SetTerrain(TerrainHolder terrain) {
        upperBoundry = terrain.upBoundry;
        lowerBoundry = terrain.downBoundry;
        leftBoundry = terrain.leftBoundry;
        rightBoundry = terrain.rightBoundry;

        isSet = true;

        calculateBoundries();
    }

    void calculateBoundries() {
        if (isSet == true) {
            if (cam.orthographic == false) {

                float verticleCamClamp = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad / 2) * cam.transform.position.y;
                float horizontalCamClamp = verticleCamClamp * ((float)Screen.width / (float)Screen.height);

                maxZ = upperBoundry.position.z - verticleCamClamp;
                minZ = lowerBoundry.position.z + verticleCamClamp;
                maxX = rightBoundry.position.x - horizontalCamClamp;
                minX = leftBoundry.position.x + horizontalCamClamp;

            } else {

                float verticleCamClamp = cam.orthographicSize;
                float horizontalCamClamp = verticleCamClamp * ((float)Screen.width / (float)Screen.height);

                maxZ = upperBoundry.position.z - verticleCamClamp;
                minZ = lowerBoundry.position.z + verticleCamClamp;
                maxX = rightBoundry.position.x - horizontalCamClamp;
                minX = leftBoundry.position.x + horizontalCamClamp;

            }
        }
    }

}
