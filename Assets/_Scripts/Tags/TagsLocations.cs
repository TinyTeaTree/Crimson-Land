using UnityEngine;
using System.Collections;

public class TagsLocations {

    public static TagsLocations loginMenu = new TagsLocations("Login Menu");
    public static TagsLocations mainMenu = new TagsLocations("Main Menu");
    public static TagsLocations battleground = new TagsLocations("Battleground");
    public static TagsLocations gameOver = new TagsLocations("GameOver");

    TagsLocations(string sceneName) {
        this._sceneName = sceneName;
    }

    string _sceneName;
    public string sceneName {
        get { return _sceneName; }
    }

}
