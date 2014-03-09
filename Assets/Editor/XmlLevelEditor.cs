using UnityEngine;
using UnityEditor;

public class XmlLevelEditor : EditorWindow {

	DeserializedLevelsLoader deserializedLevelsLoader;
	DeserializedLevelsSaver  deserializedLevelsSaver;

	[MenuItem("Window/Xml Level Editor")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(XmlLevelEditor));
	}
	
	void OnGUI()
	{
		// create one DeserializedLevelsLoader and Saver instance
		if (deserializedLevelsLoader == null) deserializedLevelsLoader = new DeserializedLevelsLoader();
		if (deserializedLevelsSaver  == null) deserializedLevelsSaver  = new DeserializedLevelsSaver();

		GUILayout.Label ("Import", EditorStyles.boldLabel);
		GUILayout.Label ("Import Levels.xml into the scene");

		if (GUILayout.Button("Import Levels.xml"))
		{
			deserializedLevelsLoader.generateItems();

			//DeserializedLevels levelsXml = XmlIO.LoadLevels();

		}



		GUILayout.Label ("Export", EditorStyles.boldLabel);
		GUILayout.Label ("Export children of \"" + DeserializedLevelsSaver.xmlItemsToExportGOName +"\" GameObject into " + DeserializedLevelsSaver.xmlItemsToExportGOName +".xml", EditorStyles.wordWrappedLabel);
		if (GUILayout.Button("Export " + DeserializedLevelsSaver.xmlItemsToExportGOName))
		{
			deserializedLevelsSaver.saveExportItems();

		}


		GUILayout.Label ("Delete", EditorStyles.boldLabel);

		GUILayout.Label ("Delete " + DeserializedLevelsLoader.xmlItemsGOName + " and " + DeserializedLevelsSaver.xmlItemsToExportGOName + " GameObjects from scene", EditorStyles.wordWrappedLabel);
		if (GUILayout.Button("Delete"))
		{
			DestroyImmediate(GameObject.Find (DeserializedLevelsLoader.xmlItemsGOName));
			DestroyImmediate(GameObject.Find (DeserializedLevelsSaver.xmlItemsToExportGOName));
		}

		/*GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);
		
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();*/
	}

}
