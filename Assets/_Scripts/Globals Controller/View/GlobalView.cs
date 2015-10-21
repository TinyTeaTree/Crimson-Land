using UnityEngine;
using System.Collections;

public class GlobalView : MonoBehaviour {

    [HideInInspector]
    public BlackOut blackOut;

    void Awake() {
        blackOut = GetComponentInChildren<BlackOut>();
    }

}
