using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UserData {
    public string username;

    public UserData(string username) {
        this.username = username;
    }

    public override bool Equals(object obj) {
        return username == ((UserData)obj).username;
    }

    //so the warning wont complain
    public override int GetHashCode() {
        return username.GetHashCode();
    }
}

[System.Serializable]
public class UsersFragmentData {
    public List<UserData> usernames;
}

public class UsersFragment : SaveFragment {

    [SerializeField]
    UsersFragmentData data;

    public List<UserData> GetUsernames() {
        return data.usernames;
    }

    public void AddUsername(string newUser) {
        data.usernames.Add(new UserData(newUser));
        newInfoPresent = true;
    }

    public void DeleteUsername(string doomedUser) {
        data.usernames.Remove(new UserData(doomedUser));
        newInfoPresent = true;
    }

    public bool HasUsername(string username) {
        return data.usernames.Contains(new UserData(username));
    }

    public override object Serialize() {
        return data;
    }

    public override void Deserialize(object graph) {
        if (graph != null) {
            UsersFragmentData frag = graph as UsersFragmentData;
            this.data = frag;
        }
    }

    public override void Reset() {
        data.usernames = new List<UserData>();
        newInfoPresent = false;
    }
}
