using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class KeyCodeDriver : MonoBehaviour {
	
    [System.Serializable]
    public class EventKeyPair {
        public UnityEvent keyDownEvent;
        public UnityEvent keyUpEvent;
        public KeyCode key;

        public EventKeyPair(KeyCode key, System.Action downEvent, System.Action upEvent) {
            this.key = key;
            keyDownEvent = new UnityEvent();
            //keyDownEvent.AddListener

        }
    }

    [SerializeField]
    List<EventKeyPair> keyInputs;

    bool alive = true;

	void Start () {
        foreach (EventKeyPair pair in keyInputs) {
            if (pair.key != KeyCode.None) {
                if (pair.keyDownEvent != null && pair.keyDownEvent.GetPersistentEventCount() > 0) {
                    StartCoroutine(checkForKeyDown(pair.key, pair.keyDownEvent));
                }
                if (pair.keyUpEvent != null && pair.keyUpEvent.GetPersistentEventCount() > 0) {
                    StartCoroutine(checkForKeyUp(pair.key, pair.keyUpEvent));
                }
            } else {
                Debug.LogWarning("Error: KeyCode none does can not be used in the KeyCodeDriver");
            }
        }
	}

    public void AddKeyCodeUpListener(KeyCode key, System.Action onKeyUpAction) {
        EventKeyPair pair = getPairByKey(key);

        if (pair == null) { //means we add a new pair

        }

        //pair = new EventKeyPair();
       // pair.key = key;
       // pair.
        //keyInputs.Add(new EventKeyPair());
    }

    public void AddKeyCodeDownListener(KeyCode key, System.Action onKeyDownAction) {

    }

    public void ResetKeyCodeUp(KeyCode key) {

    }

    public void ResetKeyCodeDown(KeyCode key) {

    }

    public void ResetAllKeyCodes() {

    }

    IEnumerator checkForKeyDown(KeyCode key, UnityEvent keyDownEvent) {
        while (alive) {
            if (Input.GetKeyDown(key) == true) {
                keyDownEvent.Invoke();
            }
            yield return null;
        }
    }

    IEnumerator checkForKeyUp(KeyCode key, UnityEvent keyUpEvent) {
        while (alive) {
            if (Input.GetKeyUp(key) == true) {
                keyUpEvent.Invoke();
            }
            yield return null;
        }
    }

    EventKeyPair getPairByKey(KeyCode key) {
        foreach (EventKeyPair pair in keyInputs) {
            if (pair.key == key) {
                return pair;
            }
        }
        return null;
    }
    
    void OnDestroy() {
        alive = false;
        StopAllCoroutines();

        foreach (EventKeyPair pair in keyInputs) {
            pair.keyUpEvent.RemoveAllListeners();
            pair.keyUpEvent = null;

            pair.keyDownEvent.RemoveAllListeners();
            pair.keyDownEvent = null;
        }

        keyInputs = null;
    }
	
}
