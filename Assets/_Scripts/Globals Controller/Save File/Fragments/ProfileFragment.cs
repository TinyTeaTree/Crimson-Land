using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class ProfileFragmentData {
    public string id;
}

public class ProfileFragment : SaveFragment {

    [SerializeField]
    ProfileFragmentData data;

    public string id {
        get { return data.id; }
        set { data.id = value; newInfoPresent = true; }
    }

    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            ProfileFragmentData frag = graph as ProfileFragmentData;
            this.data = frag;
        }
    }


    public override void Reset() {
        base.Reset();
        data.id = "";
    }
}
