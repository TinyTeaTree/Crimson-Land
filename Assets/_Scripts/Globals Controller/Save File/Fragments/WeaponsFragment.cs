using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponsFragmentData {
    public List<int> ownedWeapons;
}

public class WeaponsFragment : SaveFragment {

    public WeaponsFragmentData data;
    public WeaponsFragmentData startingData;

    void Awake() {
        setUpStartingData();
    }

    public override object Serialize() {
        return data;
    }

    public bool HasWeapon(int index) {
        for (int i=0; i<data.ownedWeapons.Count; ++i) {
            if (index == data.ownedWeapons[i]) {
                return true;
            }
        }
        return false;
    }

    public void AddWeapon(int index) {
        if (HasWeapon(index) == true) {
            Debug.LogError("ERROR ADD WEAPON: this weapon is already owned");
            return;
        }

        data.ownedWeapons.Add(index);
        newInfoPresent = true;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            data = graph as WeaponsFragmentData;
        }
    }

    public override void Reset() {
        base.Reset();
        setUpStartingData();
    }

    void setUpStartingData() {
        data.ownedWeapons.Clear();
        data.ownedWeapons.AddRange(startingData.ownedWeapons);
    }

}
