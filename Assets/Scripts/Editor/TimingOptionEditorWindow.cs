using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimingOptionEditorWindow : EditorWindow
{
    private TimingOption timingOption;
    private SerializedObject serializedObject;

    private List<TimingWindow> toRemove;

    public TimingOptionEditorWindow()
    {
        toRemove = new List<TimingWindow>();
    }

    [MenuItem("Window/Timing Option")]
    public static TimingOptionEditorWindow ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        return (TimingOptionEditorWindow)EditorWindow.GetWindow(typeof(TimingOptionEditorWindow));
    }

    public void OnGUI()
    {
        ShowEditor(timingOption);

        RemoveWindows();
    }

    public void ShowEditor(TimingOption option)
    {
        option.optionName = EditorGUILayout.TextField("Name", option.optionName);
        option.breaksCombo = EditorGUILayout.Toggle("Breaks Combo", option.breaksCombo);
        option.associatedColor = EditorGUILayout.ColorField("Associated Color", option.associatedColor);
        if (GUILayout.Button("Add Window"))
        {
            option.windows.Add(new TimingWindow(0, 0));
        }
        int count = 1;
        EditorGUI.indentLevel += 1;
        foreach (TimingWindow window in option.windows)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(count.ToString());
            if (GUILayout.Button("Remove"))
            {
                toRemove.Add(window);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel += 1;
            window.window = EditorGUILayout.FloatField("Window", window.window);
            window.beatOffset = EditorGUILayout.FloatField("Offset", window.beatOffset);
            EditorGUI.indentLevel -= 1;

            count++;
        }
        EditorGUI.indentLevel -= 1;
    }

    private void RemoveWindows()
    {
        foreach (TimingWindow window in toRemove)
        {
            timingOption.windows.Remove(window);
        }
        toRemove.Clear();
    }

    public void SetOption(TimingOption option)
    {
        timingOption = option;
    }
}
