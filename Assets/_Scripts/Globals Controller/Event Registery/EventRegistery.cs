using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EEventRegistrationType{

    SCREEN_RES_CHANGED,
    KILL_SOMETHING,
    AVATAR_KILLED,
    AVATAR_HIT
}

public class EventRegistration {
    public System.Action action;
    public bool destroyOnLoad;
    public bool isOneTime;
    public MonoBehaviour signature;

    public EventRegistration(System.Action action, bool destroyOnLoad, bool isOneTime, MonoBehaviour signature) {
        this.isOneTime = isOneTime;
        this.action = action;
        this.destroyOnLoad = destroyOnLoad;
        this.signature = signature;
    }

}

public class EventRegistery : MonoBehaviour {

    Dictionary<EEventRegistrationType, List<EventRegistration>> registeryDic = new Dictionary<EEventRegistrationType, List<EventRegistration>>();

    void OnLevelWasLoaded() {

        List<EEventRegistrationType> doomedLists = new List<EEventRegistrationType>();

        foreach (KeyValuePair<EEventRegistrationType, List<EventRegistration>> pair in registeryDic) {

            List<EventRegistration> doomedRegistrations = new List<EventRegistration>();

            foreach (EventRegistration registration in pair.Value) {
                if (registration.destroyOnLoad == true) {
                    doomedRegistrations.Add(registration);
                }
            }

            for (int i=0; i<doomedRegistrations.Count; ++i) {
                pair.Value.Remove(doomedRegistrations[i]);
            }

            if (pair.Value.Count == 0) {
                doomedLists.Add(pair.Key);
            }

        }

        for (int i=0; i<doomedLists.Count; ++i) {
            registeryDic.Remove(doomedLists[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventType"> The type of event to attach registration to, whenever this event happenes someone is supposed to activate it. which in turn will activate the callback provided</param>
    /// <param name="action"> The action to happened every time that the eventType is activated</param>
    /// <param name="destroyOnLoad"> if the scene is changed, should the registery be removed?</param>
    /// <param name="isOneTime"> is this a one time activation registration? meaning that after one activation no more are reuired</param>
    /// <param name="signature"> if you want to manually remove the registration later on you must provide a signiture of your script by which it will be identified later</param>
    public void AddRegisteration(EEventRegistrationType eventType, System.Action action, bool destroyOnLoad, bool isOneTime, MonoBehaviour signature = null) {
        if (action == null) {
            Debug.LogError("ERROR: Null action given for registration of type " + eventType);
            return;
        }

        if (registeryDic.ContainsKey(eventType)) {
            List<EventRegistration> list = null;
            if (registeryDic.TryGetValue(eventType, out list) == true) {
                list.Add(new EventRegistration(action, destroyOnLoad, isOneTime, signature));
            }
        } else {
            List<EventRegistration> newRegistery = new List<EventRegistration>();
            newRegistery.Add(new EventRegistration(action, destroyOnLoad, isOneTime, signature));
            registeryDic.Add(eventType, newRegistery);
        }
    }

    public void ActivateEvent(EEventRegistrationType eventType) {
        List<EventRegistration> registrations = null;

        bool removeList = false;

        if (registeryDic.TryGetValue(eventType, out registrations)) {

            List<EventRegistration> doomedOneTimers = new List<EventRegistration>();

            foreach (EventRegistration registration in registrations) {
                registration.action();
                if (registration.isOneTime == true) {
                    doomedOneTimers.Add(registration);
                }
            }

            for (int i=0; i<doomedOneTimers.Count; ++i) {
                registrations.Remove(doomedOneTimers[i]);
            }

            if (registrations.Count == 0) {
                removeList = true;
            }

        }

        if (removeList == true) {
            registeryDic.Remove(eventType);
        }
    }

    public void RemoveResistration(MonoBehaviour signature) {
        if(signature == null ){
            return;
        }

        List<EEventRegistrationType> doomedTypes = new List<EEventRegistrationType>();

        foreach (KeyValuePair<EEventRegistrationType, List<EventRegistration>> pair in registeryDic) {

            List<EventRegistration> doomedRegistration = new List<EventRegistration>();

            foreach (EventRegistration registration in pair.Value) {
                if (registration.signature == signature) {
                    doomedRegistration.Add(registration);
                }
            }

            for (int i=0; i<doomedRegistration.Count; ++i) {
                pair.Value.Remove(doomedRegistration[i]);
            }

            if (pair.Value.Count == 0) {
                doomedTypes.Add(pair.Key);
            }

        }

        for (int i=0; i<doomedTypes.Count; ++i) {
            registeryDic.Remove(doomedTypes[i]);
        }
    }

}
