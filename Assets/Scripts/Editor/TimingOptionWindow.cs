using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TimingOptionWindow : EditorWindow
{
    /* TODO:
     * √ sort the list
     * √ make an editor for the option
     * √ make the buttons linear and reactive to the grid
     * √ number of marker lines field
     * √ remove buttons
     * √ add button
     * √ multiple windows on a timing option
     * √ get rid of hard coded option button sizing
     * button for testing the holes in the options
     * √ button to swap to list view
     * √ duplicate button
     */

    private const float goodWordButtonHeight = 20f;

    private TempoConfig theConfig;
    private TimingOption toRemove;
    private TimingOption toUndo;

    private bool isBeatView = true;

    private int numberOfLines = 16;

    private Vector2 scrollPos;

    TimingOptionWindow()
    {
        scrollPos = new Vector2();
    }

    public void SetConfig(TempoConfig config)
    {
        theConfig = config;
    }

    public static TimingOptionWindow Construct()
    {
        return CreateInstance<TimingOptionWindow>();
    }

    [MenuItem("Timing Options")]
    void OnGUI()
    {
        if (isBeatView)
        {
            ShowBeatView();
        }
        else
        {
            ShowListView();
        }
        RemoveDeletedOption();
    }

    private void ShowListView()
    {
        EditorGUILayout.BeginScrollView(scrollPos);
        foreach(TimingOption option in theConfig.timingOptions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(option.optionName);
            if (GUILayout.Button("Edit"))
            {
                TimingOptionEditorWindow.ShowWindow().SetOption(option);
            }
            if (GUILayout.Button("Remove"))
            {
                toRemove = option;
            }
            EditorGUILayout.EndHorizontal();
        }
        DoShowViewButton(new Rect());
        EditorGUILayout.EndScrollView();
    }

    private void ShowBeatView()
    {
        float originX = 30;
        float originY = 30;
        float width = position.width - (originX * 2);
        float height = position.height - (originY * 2);

        Rect frame = new Rect(originX, originY, width, height);

        DrawGraph(frame);
        DrawLabels(frame);

        if (theConfig != null)
        {
            DoShowTimingOptions(frame);

            DoLineMarkerField(frame);

            DoAddButton(frame);

            DoUndoButton(frame);

            DoShowViewButton(frame);
        }
    }

    private void DoShowViewButton(Rect frame)
    {
        if (isBeatView)
        {
            float undoWidth = 120;
            float undoHeight = goodWordButtonHeight;
            if (GUI.Button(new Rect(frame.x + frame.width - undoWidth, frame.y + frame.height - undoHeight, undoWidth, undoHeight), "Show List View"))
            {
                isBeatView = !isBeatView;
            }
        }
        else
        {
            if(GUILayout.Button("Show Beat View"))
            {
                isBeatView = !isBeatView;
            }
        }
    }

    private void DoShowTimingOptions(Rect frame)
    {
        float btnHeight = frame.height / 10;
        if (btnHeight < 15)
        {
            btnHeight = 15;
        }

        foreach (TimingOption option in theConfig.timingOptions)
        {
            foreach(TimingWindow window in option.windows)
            {
                GUI.color = option.associatedColor;
                float currentButtonWidth = GetButtonWidth(window.window, frame.width);
                float currentButtonHeight = btnHeight;
                float xPos = frame.x + GetXPos(window.beatOffset, frame.width);
                float yPos = frame.y + (frame.height / 2) - (currentButtonHeight / 2);

                if (GUI.Button(new Rect(xPos, yPos, currentButtonWidth, currentButtonHeight), option.optionName))
                {
                    TimingOptionEditorWindow.ShowWindow().SetOption(option);
                }

                GUI.color = Color.white;
                if (GUI.Button(new Rect(xPos + currentButtonWidth / 2 - 10, yPos + currentButtonHeight + (btnHeight / 2), 20, 20), "-"))
                {
                    toRemove = option;
                }
                if (GUI.Button(new Rect(xPos + currentButtonWidth / 2 - 10, yPos - (btnHeight / 2) - 20, 20, 20), "D"))
                {
                    DuplicateOption(option);
                }
            }
        }
    }

    private void DoAddButton(Rect pos)
    {
        float btnWidth = 40;
        float btnHeight = goodWordButtonHeight;
        if (GUI.Button(new Rect(pos.x + pos.width / 2 - btnWidth / 2, pos.y + pos.height - btnHeight, btnWidth, btnHeight), "Add"))
        {
            TimingOption newBoy = new TimingOption();
            theConfig.timingOptions.Add(newBoy);
            // show the editor dialog
            TimingOptionEditorWindow.ShowWindow().SetOption(newBoy);

            // also clear the deleted thing
            toUndo = null;
        }
    }

    private void DoUndoButton(Rect pos)
    {
        if (toUndo != null)
        {
            float undoWidth = 80;
            float undoHeight = goodWordButtonHeight;
            if (GUI.Button(new Rect(pos.x + pos.width * 0.75f - undoWidth / 2, pos.y + pos.height - undoHeight, undoWidth, undoHeight), "Undo Delete"))
            {
                theConfig.timingOptions.Add(toUndo);
                toUndo = null;
            }
        }
    }

    private void DoLineMarkerField(Rect pos)
    {
        float labelWidth = 40;
        float labelHeight = goodWordButtonHeight;
        float btnWidth = 20;
        float btnHeight = goodWordButtonHeight;
        EditorGUI.LabelField(new Rect(pos.x, pos.y + pos.height - labelHeight, labelWidth, labelHeight), "Lines:");
        numberOfLines = EditorGUI.IntField(new Rect(pos.x + labelWidth, pos.y + pos.height - btnHeight, btnWidth, btnHeight), numberOfLines);
        if (numberOfLines > 50)
        {
            numberOfLines = 50;
        }
        else if (numberOfLines < 0)
        {
            numberOfLines = 0;
        }
    }

    private void DrawGraph(Rect pos)
    {
        Handles.BeginGUI();
        Handles.color = Color.black;
        foreach (KeyValuePair<float, float> pair in GetDottedLinePositions(pos.x, pos.width, numberOfLines, pos.height / 4))
        {
            float yStart = pos.y + (pos.height - pair.Value) / 2;
            Handles.DrawDottedLine(new Vector3(pair.Key, yStart), new Vector3(pair.Key, yStart + pair.Value), 5);
        }
        foreach (KeyValuePair<float, float> pair in GetLinePositions(pos))
        {
            float yStart = pos.y + (pos.height - pair.Value) / 2;
            Handles.DrawLine(new Vector3(pair.Key, yStart), new Vector3(pair.Key, yStart + pair.Value));
        }
        Handles.EndGUI();
    }

    private List<KeyValuePair<float, float>> GetLinePositions(Rect pos)
    {
        List<KeyValuePair<float, float>> result = new List<KeyValuePair<float, float>>();

        float halfHeight = pos.height / 2;

        // left line
        result.Add(new KeyValuePair<float, float>(pos.x, halfHeight));

        // middle line
        result.Add(new KeyValuePair<float, float>(pos.x + pos.width / 2, pos.height));

        // right line
        result.Add(new KeyValuePair<float, float>(pos.x + pos.width, halfHeight));

        return result;
    }

    private List<KeyValuePair<float, float>> GetDottedLinePositions(float x, float width, int divisions, float minHeight)
    {
        List<KeyValuePair<float, float>> result = new List<KeyValuePair<float, float>>();

        float widthDiv = width / divisions;
        float currentDiv = x;
        bool changeLineHeight = divisions % 2 == 0;
        bool bigLine = true;

        for (int i = 0; i <= divisions; i++)
        {
            result.Add(new KeyValuePair<float, float>(currentDiv, bigLine ? minHeight * 1.5f : minHeight));
            currentDiv += widthDiv;
            if (changeLineHeight)
            {
                bigLine = !bigLine;
            }
        }
        return result;
    }

    private void DrawLabels(Rect pos)
    {
        GUI.Label(new Rect(pos.x + (pos.width / 2) - 15, 0, 30, 30), "Beat", EditorStyles.centeredGreyMiniLabel);
        GUI.Label(new Rect(pos.x, 0, 40, 30), "Halfway", EditorStyles.centeredGreyMiniLabel);
        GUI.Label(new Rect(pos.x + pos.width - 40, 0, 40, 30), "Halfway", EditorStyles.centeredGreyMiniLabel);
    }

    private float GetXPos(double offset, float width)
    {
        double posMultiplier = offset + 0.5;
        return (float)(width * posMultiplier);
    }

    private float GetButtonWidth(double offset, float width)
    {
        return (float)(width * offset);
    }

    private void DuplicateOption(TimingOption option)
    {
        TimingOption newBoy = new TimingOption(option);
        theConfig.timingOptions.Add(newBoy);
        // show the editor dialog
        TimingOptionEditorWindow.ShowWindow().SetOption(newBoy);
    }

    private void RemoveDeletedOption()
    {
        if (toRemove != null)
        {
            toUndo = toRemove;
            theConfig.timingOptions.Remove(toRemove);
            toRemove = null;
        }
    }
}
