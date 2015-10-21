using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ComponentDocumentation))]
public class DocumentationInspector : Editor {

    public override void OnInspectorGUI() {

        serializedObject.Update();


        serializedObject.FindProperty("documentation").stringValue = EditorGUILayout.TextArea(serializedObject.FindProperty("documentation").stringValue);


        serializedObject.ApplyModifiedProperties();

    }

}
