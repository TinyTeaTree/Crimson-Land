using UnityEngine;
using System.Collections;

public class TerrainHolder : MonoBehaviour {

    public Transform leftBoundry, rightBoundry, upBoundry, downBoundry;

    void Start() {
        transform.ResetTransform();
        BattlegroundController controller = GameObject.FindGameObjectWithTag(Tags.BattlegroundController).GetComponent<BattlegroundController>();

        if (controller.terrain != null) {
            Destroy(controller.terrain.gameObject);
        }
        controller.SetUpTerrain(this);
    }

}
