using UnityEngine;
using UnityEditor;

public class XmlLevelEditor : EditorWindow {

	DeserializedLevelsLoader 		deserializedLevelsLoader;
	DeserializedLevelsSaver  		deserializedLevelsSaver;
	DeserializedLevelsCrossChecker	deserializedLevelsCrossChecker;

	[MenuItem("Window/Xml Level Editor")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(XmlLevelEditor));
	}
	
	void OnGUI()
	{
		// create one DeserializedLevelsLoader and Saver instance
		if (deserializedLevelsLoader 		== null) deserializedLevelsLoader 		= new DeserializedLevelsLoader();
		if (deserializedLevelsSaver  		== null) deserializedLevelsSaver  		= new DeserializedLevelsSaver();
		if (deserializedLevelsCrossChecker	== null) deserializedLevelsCrossChecker	= new DeserializedLevelsCrossChecker();


		// Import section
		GUILayout.Label ("Import", EditorStyles.boldLabel);
		GUILayout.Label ("Import Levels.xml into the scene");

		if (GUILayout.Button("Import Levels.xml"))
			deserializedLevelsLoader.generateItems();


		// Export section
		GUILayout.Label ("Export", EditorStyles.boldLabel);
		GUILayout.Label ("Export children of \"" + DeserializedLevelsSaver.xmlItemsToExportGOName +"\" GameObject into " + DeserializedLevelsSaver.xmlItemsToExportGOName +".xml", EditorStyles.wordWrappedLabel);
		if (GUILayout.Button("Export " + DeserializedLevelsSaver.xmlItemsToExportGOName))
			deserializedLevelsSaver.saveExportItems();


		// Delete section
		GUILayout.Label ("Delete", EditorStyles.boldLabel);
		GUILayout.Label ("Delete " + DeserializedLevelsLoader.xmlItemsGOName + " and " + DeserializedLevelsSaver.xmlItemsToExportGOName + " GameObjects from scene", EditorStyles.wordWrappedLabel);
		if (GUILayout.Button("Delete"))
		{
			DestroyImmediate(GameObject.Find (DeserializedLevelsLoader.xmlItemsGOName));
			DestroyImmediate(GameObject.Find (DeserializedLevelsSaver.xmlItemsToExportGOName));
		}


		// Cross check section
		GUILayout.Label ("Cross Check", EditorStyles.boldLabel);
		GUILayout.Label ("Cross check /Resources/Prefabs and Levels.xml if there are any item prefabs that exist only in one but not the other", EditorStyles.wordWrappedLabel);
		if (GUILayout.Button("Cross Check"))
			deserializedLevelsCrossChecker.crossCheck();
	}

}
