using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {

    bool isTransitioning = false;

    TagsLocations _currentLocation = TagsLocations.loginMenu;
    public TagsLocations currentLocation {
        get { return _currentLocation; }
    }

    public void GoTo(TagsLocations location) {
        if (isTransitioning == false) {
            isTransitioning = true;
            GlobalController.instance.view.blackOut.FadeOutIn(() => {
                Application.LoadLevel(location.sceneName);
                isTransitioning = false;
            });
        } else {
            Debug.LogError("ERROR GOTO: already in transition");
        }
    }


}
