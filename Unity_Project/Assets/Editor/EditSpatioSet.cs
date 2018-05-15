using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpatioSet))]
public class EditSpatioSet : Editor {
    public override void OnInspectorGUI()
    {
        SpatioSet ss = target as SpatioSet;

        GUILayout.Label("SpatioSet Controls");
        ss.slider = (Slider)EditorGUILayout.ObjectField("Timeline Slider", ss.slider, typeof(Slider), true);
        ss.InfoText = (Text)EditorGUILayout.ObjectField("Info Text", ss.InfoText, typeof(Text), true);
        ss.DateText = (Text)EditorGUILayout.ObjectField("Date Text", ss.DateText, typeof(Text), true);
        ss.TestText = (Text)EditorGUILayout.ObjectField("Test Text", ss.TestText, typeof(Text), true);
        ss.Description = (string)EditorGUILayout.TextField("Description", ss.Description);
    }
}
