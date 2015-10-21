using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CharacterFragment))]
public class CharacterFragmentEditor : Editor {

    LevelData levelData;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CharacterFragment fragment = target as CharacterFragment;

        if (levelData == null) {
            levelData = GameObject.FindObjectOfType<LevelData>().GetComponent<LevelData>();
        }

        int currentLevel = levelData.GetLevel(fragment.xp);

        GUILayout.Label("Level " + currentLevel.ToString());
    }

}
