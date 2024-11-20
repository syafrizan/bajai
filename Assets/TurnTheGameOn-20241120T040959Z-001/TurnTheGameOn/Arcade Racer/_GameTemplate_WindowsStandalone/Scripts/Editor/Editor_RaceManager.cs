﻿namespace TurnTheGameOn.ArcadeRacer
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditorInternal;

    [CustomEditor(typeof(RaceManager))]
    public class Editor_RaceManager : Editor
    {
        RaceManager circuit;
        ReorderableList reorderableList;
        float lineHeight;
        float lineHeightSpace;
        private bool showGizmoSettings;
        private bool showWaypoints;
        private bool showAutoAssignJunctionHelper;
        private RaceManager autoJunctionGetPointsTarget;
        private bool canShowAutoAssignJunctionsButton;
        private bool didAutoConfigureJunctions;

        void OnEnable()
        {
            lineHeight = EditorGUIUtility.singleLineHeight;
            lineHeightSpace = lineHeight + 0;

            circuit = (RaceManager)target;
            reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("waypointDataList"), false, true, false, true);
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, lineHeight), element.FindPropertyRelative("_name").stringValue);
                EditorGUI.indentLevel = 7;
                GUI.enabled = false;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + (lineHeightSpace * 0), rect.width, lineHeight), element.FindPropertyRelative("_transform"));
                GUI.enabled = true;
                EditorGUI.indentLevel = 0;
                reorderableList.elementHeightCallback = (int _index) => lineHeightSpace * 2;
            };
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, lineHeight), "Waypoints");
            };
            reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                GameObject go = circuit.waypointDataList[reorderableList.index]._transform.gameObject;
                circuit.waypointDataList.RemoveAt(reorderableList.index);
            };
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((RaceManager)target), typeof(RaceManager), false);
            GUI.enabled = true;

            serializedObject.Update();
            EditorGUILayout.BeginVertical(EditorStyles.inspectorFullWidthMargins);
            EditorStyles.label.wordWrap = true;

            EditorGUILayout.HelpBox
                (
                "- Alt + Left Click in scene view on a Collider to add new points to the route \n" +
                "- Alt + Ctrl + Left Click in scene view on a Collider to insert a new point between 2 points"
                , MessageType.None
                );

            SerializedProperty defaultCamera = serializedObject.FindProperty("defaultCamera");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(defaultCamera, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty raceTimerAudioClip = serializedObject.FindProperty("raceTimerAudioClip");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(raceTimerAudioClip, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty onRaceFinished = serializedObject.FindProperty("onRaceFinished");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(onRaceFinished, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            showWaypoints = EditorGUILayout.Foldout(showWaypoints, "Waypoints", true);
            if (showWaypoints)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUI.BeginChangeCheck();
                reorderableList.DoLayoutList();
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
                EditorGUILayout.EndVertical();
            }

            SerializedProperty spawnPoints = serializedObject.FindProperty("spawnPoints");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(spawnPoints, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();
        }

        void OnSceneGUI()
        {
            RaceManager waypointManager = (RaceManager)target;

            for (int i = 0; i < waypointManager.waypointDataList.Count; i++)
            {
                if (waypointManager.waypointDataList[i]._waypoint)
                {
                    Handles.Label(waypointManager.waypointDataList[i]._waypoint.transform.position + new Vector3(0, 0.25f, 0),
                    "    Waypoint: " + waypointManager.waypointDataList[i]._waypoint.waypointNumber.ToString()
                    );
                }
            }

            // Handles.BeginGUI();
            // if (GUILayout.Button("Test Button", GUILayout.Width(100)))
            // {
            // 	//
            // }
            // Handles.EndGUI();

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && e.alt && e.button == 0 && e.control)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    serializedObject.UpdateIfRequiredOrScript();
                    waypointManager.ClickToInsertSpawnNextWaypoint(hitInfo.point);
                    serializedObject.UpdateIfRequiredOrScript();
                }
            }
            else if (e.type == EventType.MouseDown && e.button == 0 && e.alt)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    serializedObject.UpdateIfRequiredOrScript();
                    waypointManager.ClickToSpawnNextWaypoint(hitInfo.point);
                    serializedObject.UpdateIfRequiredOrScript();
                }
            }

        }

    }
}