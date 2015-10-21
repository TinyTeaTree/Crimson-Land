using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace SphereAndCube_BestFriendsForever {

    /// <summary>
    /// Is responsible to check for inputs of mouse.
    /// </summary>
    public class InputMouseDriver : MonoBehaviour {

        public UnityEvent onMouseButtonLeftUpEvent;
        public UnityEvent onMouseButtonLeftDownEvent;

        public UnityEvent onMouseButtonRightUpEvent;
        public UnityEvent onMouseButtonRightDownEvent;

        bool alive = true;

        void Start() {
            if (onMouseButtonLeftUpEvent.GetPersistentEventCount() > 0) {
                StartCoroutine(checkForMouseUpInputs());
            }
            if (onMouseButtonLeftDownEvent.GetPersistentEventCount() > 0) {
                StartCoroutine(checkForMouseDownInputs());
            }
            if (onMouseButtonRightUpEvent.GetPersistentEventCount() > 0) {
                StartCoroutine(checkForMouseUp2Inputs());
            }
            if (onMouseButtonRightDownEvent.GetPersistentEventCount() > 0) {
                StartCoroutine(checkForMouseDown2Inputs());
            }
        }

        IEnumerator checkForMouseUpInputs() {
            while (alive) {
                if (Input.GetMouseButtonUp(0) == true) {
                    onMouseButtonLeftUpEvent.Invoke();
                }

                yield return null;
            }
        }

        IEnumerator checkForMouseDownInputs() {
            while (alive) {
                if (Input.GetMouseButtonDown(0) == true) {
                    onMouseButtonLeftDownEvent.Invoke();
                }

                yield return null;
            }
        }

        IEnumerator checkForMouseUp2Inputs() {
            while (alive) {
                if (Input.GetMouseButtonUp(1) == true) {
                    onMouseButtonRightUpEvent.Invoke();
                }

                yield return null;
            }
        }

        IEnumerator checkForMouseDown2Inputs() {
            while (alive) {
                if (Input.GetMouseButtonDown(1) == true) {
                    onMouseButtonRightDownEvent.Invoke();
                }

                yield return null;
            }
        }

        void OnDestroy() {
            alive = false;

            onMouseButtonLeftUpEvent.RemoveAllListeners();
            onMouseButtonLeftUpEvent = null;

            onMouseButtonLeftDownEvent.RemoveAllListeners();
            onMouseButtonLeftDownEvent = null;

            onMouseButtonRightUpEvent.RemoveAllListeners();
            onMouseButtonRightUpEvent = null;

            onMouseButtonRightDownEvent.RemoveAllListeners();
            onMouseButtonRightDownEvent = null;
        }

    }

}



