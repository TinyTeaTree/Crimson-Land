using UnityEngine;
using System.Collections;

public class ScreenChecker : MonoBehaviour {

    //public Camera cam;
    EventRegistery registery;

    float width, height/*, aspect*/;

    void Awake() {
        registery = transform.parent.GetComponent<EventRegistery>();
    }

    void Start() {
        width = Screen.width;
        height = Screen.height;
        //aspect = cam.aspect;
    }

    void Update() {
        if (Screen.width != width || Screen.height != height /*|| aspect != cam.aspect*/) {
            width = Screen.width;
            height = Screen.height;
            //aspect = cam.aspect;

            registery.ActivateEvent(EEventRegistrationType.SCREEN_RES_CHANGED);
        }
    }

}
