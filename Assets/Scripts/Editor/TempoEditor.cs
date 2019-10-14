using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Tempo))]
public class TempoEditor : Editor
{
    List<TimingOption> toRemove = new List<TimingOption>();
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Tempo myTarget = (Tempo)target;
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
            option.name = EditorGUILayout.TextField("Name", option.name);
            option.breaksCombo = EditorGUILayout.Toggle("Breaks Combo", option.breaksCombo);
            option.associatedColor = EditorGUILayout.ColorField("Associated Color", option.associatedColor);
            if (GUILayout.Button("Remove", GUILayout.MaxWidth(50)))
            {
                toRemove.Add(option);
            }
        }

        foreach (TimingOption option in toRemove)
        {
            myTarget.timingOptions.Remove(option);
        }
        toRemove.Clear();
    }
}
