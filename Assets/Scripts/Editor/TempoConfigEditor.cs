using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(TempoConfig))]
public class TempoConfigEditor : Editor
{
    List<TimingOption> toRemove = new List<TimingOption>();
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TempoConfig myTarget = (TempoConfig)target;
        EditorGUILayout.Separator();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Timing Options");
        if (GUILayout.Button("Add Option"))
        {
            myTarget.timingOptions.Add(new TimingOption());
        }
        EditorGUILayout.EndHorizontal();
        foreach (TimingOption option in myTarget.timingOptions)
        {
            EditorGUILayout.Separator();
            // title
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(option.name);
            if (GUILayout.Button("Remove"))
            {
                toRemove.Add(option);
            }
            EditorGUILayout.EndHorizontal();
            // fields
            option.name = EditorGUILayout.TextField("Name", option.name);
            option.breaksCombo = EditorGUILayout.Toggle("Breaks Combo", option.breaksCombo);
            option.associatedColor = EditorGUILayout.ColorField("Associated Color", option.associatedColor);
            option.window = EditorGUILayout.DoubleField("Window In Beat", option.window);
            option.offsetFromBeat = EditorGUILayout.DoubleField("Offset From Beat", option.offsetFromBeat);
        }

        foreach (TimingOption option in toRemove)
        {
            myTarget.timingOptions.Remove(option);
        }
        toRemove.Clear();

        // making changes to the custom gui save
        if (GUI.changed)
        {
            EditorUtility.SetDirty(myTarget);
            EditorSceneManager.MarkSceneDirty(myTarget.gameObject.scene);
        }
    }
}
