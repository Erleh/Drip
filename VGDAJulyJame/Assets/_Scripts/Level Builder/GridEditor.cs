using System.Collections;
using System.Collections.Generic;
using jkGenerator;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(LevelBuilder))]
[CanEditMultipleObjects]
public class GridEditor : Editor
{
    private static int placeX, placeY;
    private static Object placeObj;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelBuilder builder = (LevelBuilder) target;
        if(builder.grid.GetIndexes() == null)
            builder.allowDisplay = false;
        GUILayout.Label("-------------------------------------------------");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Grid", GUILayout.ExpandWidth(false)))
            builder.Construct();
        if (GUILayout.Button("Remove objects in Grid", GUILayout.ExpandWidth(false)))
            builder.Deconstruct();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Place ", GUILayout.ExpandWidth(false));
        placeObj = EditorGUILayout.ObjectField(placeObj, typeof(GameObject), true, GUILayout.Width(150f));
        GUILayout.Label("at: ( ", GUILayout.ExpandWidth(false));
        placeX = EditorGUILayout.IntField(placeX, GUILayout.Width(20f));
        GUILayout.Label(" , ", GUILayout.ExpandWidth(false));
        placeY = EditorGUILayout.IntField(placeY, GUILayout.Width(20f));
        GUILayout.Label(" ) ", GUILayout.ExpandWidth(false));
        if(GUILayout.Button("Create"))
            builder.Place(placeX - 1, builder.grid.GetIndexes().GetLength(1) - placeY, (GameObject)placeObj);
        GUILayout.EndHorizontal();
    }

}
#endif