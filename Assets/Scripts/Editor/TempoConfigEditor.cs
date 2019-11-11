using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(TempoConfig))]
public class TempoConfigEditor : Editor
{
    TempoConfig theConfig;
    TimingOptionWindow timingWindow;

    void OnEnable()
    {
        theConfig = (TempoConfig)target;
	}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("subdivisionsPerBeat"));
        EditorGUILayout.Separator();

        if (theConfig.timingCalculation.Equals(TimingCalculations.Subdivision))
        {
            theConfig.subdivisionsPerBeat = EditorGUILayout.IntField("Subdivisions Per Beat", theConfig.subdivisionsPerBeat);
        }
        else if (theConfig.timingCalculation.Equals(TimingCalculations.Pattern))
        {
            //myTarget.pattern = EditorGUI.
        }

        if (GUILayout.Button("Open Popup"))
        {
            timingWindow = TimingOptionWindow.Construct();
            timingWindow.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            timingWindow.SetConfig(theConfig);
            timingWindow.Show();
        }

        // making changes to the custom gui save
        if (GUI.changed)
        {
            EditorUtility.SetDirty(theConfig);
            EditorSceneManager.MarkSceneDirty(theConfig.gameObject.scene);
        }
    }
    
}
