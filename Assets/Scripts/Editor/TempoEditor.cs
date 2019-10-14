using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tempo))]
public class TempoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tempo myTarget = (Tempo)target;
        EditorGUILayout.HelpBox("Yo waddup", MessageType.Error);
        EditorGUILayout.DropdownButton(new GUIContent("heyo"), FocusType.Native);
        foreach (TimingOption option in myTarget.timingOptions)
        {
            option.name = EditorGUILayout.TextField("Name", "Good");
            option.breaksCombo = EditorGUILayout.Toggle("Breaks Combo", false);
            option.associatedColor = EditorGUILayout.ColorField("Associated Color", Color.white);
        }
    }
}
