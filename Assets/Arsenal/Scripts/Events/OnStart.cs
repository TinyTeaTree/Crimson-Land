using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnStart : MonoBehaviour {

    public UnityEvent onStart;

    void Start() {
        onStart.Invoke();
    }

}
