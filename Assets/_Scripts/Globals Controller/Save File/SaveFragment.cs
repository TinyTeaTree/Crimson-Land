using UnityEngine;
using System.Collections;

abstract public class SaveFragment : MonoBehaviour {

    [HideInInspector]
    public bool newInfoPresent;

    bool _isInitialized = false;
    public bool isInitialized {
        get { return _isInitialized; }
    }

#if UNITY_EDITOR
    [ContextMenu("New Info Present")]
    public void NewInfo() {
        newInfoPresent = true;
    }
#endif

    public abstract object Serialize();

    public void InnerDeserialize(object graph) {
        Deserialize(graph);
        _isInitialized = true;
    }

    public abstract void Deserialize(object graph);

    /// <summary>
    /// Reset the fragment: meaning that this fragment is empty and does not belong to no one and ready for deserialization
    /// </summary>
    public virtual void Reset(){
        _isInitialized = false;
        newInfoPresent = false;
    }

}
