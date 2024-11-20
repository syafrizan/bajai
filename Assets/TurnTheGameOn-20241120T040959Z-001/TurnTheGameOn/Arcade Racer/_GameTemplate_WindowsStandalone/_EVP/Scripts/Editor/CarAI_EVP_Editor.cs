namespace TurnTheGameOn.ArcadeRacer
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(CarAI_EVP))]
	public class CarAI_EVP_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty vehicleController = serializedObject.FindProperty("vehicleController");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(vehicleController, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty m_AISettings = serializedObject.FindProperty("m_AISettings");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(m_AISettings, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty sensors = serializedObject.FindProperty("sensors");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(sensors, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty driveTarget = serializedObject.FindProperty("driveTarget");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(driveTarget, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty startDrivingOnStart = serializedObject.FindProperty("startDrivingOnStart");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(startDrivingOnStart, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty startDrivingTime = serializedObject.FindProperty("startDrivingTime");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(startDrivingTime, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty startingRoutePointIndex = serializedObject.FindProperty("startingRoutePointIndex");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(startingRoutePointIndex, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty detectionData = serializedObject.FindProperty("detectionData");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(detectionData, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty currentRoutePointIndex = serializedObject.FindProperty("currentRoutePointIndex");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(currentRoutePointIndex, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty customResetPoint = serializedObject.FindProperty("customResetPoint");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(customResetPoint, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty stuckResetRoute = serializedObject.FindProperty("stuckResetRoute");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(stuckResetRoute, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty stuckResetPoint = serializedObject.FindProperty("stuckResetPoint");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(stuckResetPoint, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty cautionSlowSpeed = serializedObject.FindProperty("cautionSlowSpeed");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(cautionSlowSpeed, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			CarAI vehicleAI = (CarAI)target;

			if (GUILayout.Button("Update Sensors"))
			{
				vehicleAI.UpdateSensors();
			}

		}
	}
}