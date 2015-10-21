using UnityEngine;
using System.Collections;

public class SaveFile : MonoBehaviour {


    [HideInInspector]
    public ProfileFragment profile;
    [HideInInspector]
    public CharacterFragment character;
    [HideInInspector]
    public WeaponsFragment weapons;
    [HideInInspector]
    public TreasuryFragment treasury;
    [HideInInspector]
    public BattlesFragment battles;

    PersistanceDriver persister;
    SaveFragment[] fragments;

    void Awake() {
        persister = GetComponent<PersistanceDriver>();
        fragments = GetComponentsInChildren<SaveFragment>();
        
        profile = GetComponentInChildren<ProfileFragment>();
        character = GetComponentInChildren<CharacterFragment>();
        weapons = GetComponentInChildren<WeaponsFragment>();
        treasury = GetComponentInChildren<TreasuryFragment>();
        battles = GetComponentInChildren<BattlesFragment>();
    }

#if UNITY_EDITOR
    [ContextMenu("Force Save")]
    public void _forceSave() {
        Save(true);
    }
#endif

    public bool isInitialized {
        get {

            for (int i=0; i<fragments.Length; ++i) {
                if (fragments[i].isInitialized == false) {
                    return false;
                }
            }
            return true;
        }
    }

    public void Save(bool forceSave = false) {
        for (int i=0; i<fragments.Length; ++i) {
            if (fragments[i].newInfoPresent == true || forceSave == true) {
                persister.SaveFragment(System.IO.Path.Combine(Paths.usersFolder, profile.id), fragments[i].name, fragments[i].Serialize());
                fragments[i].newInfoPresent = false;
            }
        }
    }

    public void Load(string id) {
        for (int i=0; i<fragments.Length; ++i) {
            fragments[i].InnerDeserialize(
                persister.LoadFragment  (System.IO.Path.Combine(Paths.usersFolder, id), fragments[i].name)
            );
        }
    }

    public void DeleteUser(string userID) {

        if (userID == profile.id) {
            for (int i=0; i<fragments.Length; ++i) {
                persister.DeleteFragment(System.IO.Path.Combine(Paths.usersFolder, userID), fragments[i].name);
                fragments[i].Reset();
            }
        } else {
            for (int i=0; i<fragments.Length; ++i) {
                persister.DeleteFragment(System.IO.Path.Combine(Paths.usersFolder, userID), fragments[i].name);
            }
        }

        persister.DelteFolder(System.IO.Path.Combine(Paths.usersFolder, userID));
    }

    public void Reset() {
        for (int i=0; i<fragments.Length; ++i) {
            fragments[i].Reset();
        }
    }

    public void UndoNewInfoPresent() {
        for (int i=0; i<fragments.Length; ++i) {
            fragments[i].newInfoPresent = false;
        }
    }

}
