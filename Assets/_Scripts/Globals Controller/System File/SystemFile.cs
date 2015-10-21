using UnityEngine;
using System.Collections;

public class SystemFile : MonoBehaviour {

    [HideInInspector]
    public UsersFragment usersFragment;
    [HideInInspector]
    public AchievmentFragment achievementFragment;

    PersistanceDriver persister;
    SaveFragment[] fragments;

    void Awake() {
        persister = GetComponent<PersistanceDriver>();
        fragments = GetComponentsInChildren<SaveFragment>();

        usersFragment = GetComponentInChildren<UsersFragment>();
        achievementFragment = GetComponentInChildren<AchievmentFragment>();
    }

#if UNITY_EDITOR
    [ContextMenu("force save")]
    public void forceSave() {
        Save(true);
    }
#endif

    public void Save(bool force = false) {
        for (int i=0; i<fragments.Length; ++i) {
            if (fragments[i].newInfoPresent == true || force == true) {
                persister.SaveFragment(Paths.systemFolder, fragments[i].name + "_sys", fragments[i].Serialize());
                fragments[i].newInfoPresent = false;
            }
        }
    }

    public void Load() {
        for (int i=0; i<fragments.Length; ++i) {
            fragments[i].InnerDeserialize(
                persister.LoadFragment(Paths.systemFolder, fragments[i].name + "_sys")
            );
        }
    }

}
