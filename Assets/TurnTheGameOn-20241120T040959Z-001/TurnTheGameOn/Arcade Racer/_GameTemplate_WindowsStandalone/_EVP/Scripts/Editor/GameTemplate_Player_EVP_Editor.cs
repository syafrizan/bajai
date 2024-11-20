namespace TurnTheGameOn.ArcadeRacer
{
	using UnityEditor;

	[CustomEditor(typeof(GameTemplate_Player_EVP))]
	public class GameTemplate_Player_EVP_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty vehicleController = serializedObject.FindProperty("vehicleController");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(vehicleController, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty vehicleAudio = serializedObject.FindProperty("vehicleAudio");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(vehicleAudio, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty vehicleStandardInput = serializedObject.FindProperty("vehicleStandardInput");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(vehicleStandardInput, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty controllerType = serializedObject.FindProperty("controllerType");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(controllerType, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty speedType = serializedObject.FindProperty("speedType");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(speedType, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty playerInputProfiles = serializedObject.FindProperty("playerInputProfiles");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(playerInputProfiles, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty playerInputSettings = serializedObject.FindProperty("playerInputSettings");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(playerInputSettings, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty avatarSettings = serializedObject.FindProperty("avatarSettings");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(avatarSettings, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty dashboardCanvas = serializedObject.FindProperty("dashboardCanvas");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(dashboardCanvas, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty vehicleCameraSystem = serializedObject.FindProperty("vehicleCameraSystem");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(vehicleCameraSystem, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty carNitro = serializedObject.FindProperty("carNitro");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(carNitro, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty miniMapCanvasPrefab = serializedObject.FindProperty("miniMapCanvasPrefab");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(miniMapCanvasPrefab, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty miniMapCameraPrefab = serializedObject.FindProperty("miniMapCameraPrefab");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(miniMapCameraPrefab, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty playerMiniMapIconPrefab = serializedObject.FindProperty("playerMiniMapIconPrefab");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(playerMiniMapIconPrefab, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty wrongWayWarningCanvasPrefab = serializedObject.FindProperty("wrongWayWarningCanvasPrefab");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(wrongWayWarningCanvasPrefab, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty waypointArrow = serializedObject.FindProperty("waypointArrow");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(waypointArrow, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty arrowSmooth = serializedObject.FindProperty("arrowSmooth");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(arrowSmooth, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty garageSceneName = serializedObject.FindProperty("garageSceneName");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(garageSceneName, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty disableIfInGarage = serializedObject.FindProperty("disableIfInGarage");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(disableIfInGarage, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty disableObjIfInGarage = serializedObject.FindProperty("disableObjIfInGarage");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(disableObjIfInGarage, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty disableOnPause = serializedObject.FindProperty("disableOnPause");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(disableOnPause, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty nitroON = serializedObject.FindProperty("nitroON");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(nitroON, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty nitroOFF = serializedObject.FindProperty("nitroOFF");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(nitroOFF, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			SerializedProperty frozenControls = serializedObject.FindProperty("frozenControls");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(frozenControls, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

		}
	}
}