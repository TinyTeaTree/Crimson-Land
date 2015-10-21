using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Arsenal {

    /// <summary>
    /// Clicks a button or a script implementing IPointerClickHandler automatically
    /// during start.
    /// </summary>
    public class AutoClickScript : MonoBehaviour {
        IPointerClickHandler clicker = null;

        void Awake() {
            clicker = GetComponent(typeof(IPointerClickHandler)) as IPointerClickHandler;
            if (clicker == null) {
                Debug.LogError("Couldn't find IPointerClickHandler interface on any componenet on this object " + gameObject.name);
            }
        }

        void Start() {
            if (clicker != null) {
                clicker.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }

}