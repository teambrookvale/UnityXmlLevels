using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class DeserializedLevelsCrossChecker
{
    // cross check /Resources/Prefabs and Levels.xml if there are any item prefabs that exist only in one but not the other
    public void crossCheck()
    {
        // create a two sets of prefabs
        HashSet<string> resPrefabSet = new HashSet<string>();
        HashSet<string> xmlPrefabSet = new HashSet<string>();

        // Get prefabs from Levels.xml
        getLevelPrefabs(xmlPrefabSet);

        // Get prefabs from the /Resources/Prefabs folder
        getResPrefabs(resPrefabSet);

        // Cross checks
        foreach (string prefab in xmlPrefabSet.Except(resPrefabSet).ToList())
            Debug.LogError(prefab + " is missing in the /Resorces/Prefabs folder but used in Levels.xml");

        foreach (string prefab in resPrefabSet.Except(xmlPrefabSet).ToList())
            Debug.Log(prefab + " exists in the /Resorces/Prefabs folder but not used in Levels.xml");

        Debug.Log("Cross Check Done");
    }

    private static void getResPrefabs(HashSet<string> resPrefabList)
    {
        // get all child items in the /Resources/Prefabs folder
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Prefabs");
        FileInfo[] fileInfos = dir.GetFiles("*.prefab");
        fileInfos.Select(f => f.FullName).ToArray();

        // Add each prefab's file name to prefabList and truncate the .prefab extension from the end
        foreach (FileInfo fileInfo in fileInfos)
        {
            resPrefabList.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length - ".prefab".Length));
        }
    }

    private static void getLevelPrefabs(HashSet<string> xmlPrefabSet)
    {
        DeserializedLevels deserializedLevels = XmlIO.LoadXml<DeserializedLevels>("Levels");
        foreach (DeserializedLevels.Level level in deserializedLevels.levels)
        {
            foreach (DeserializedLevels.Item item in level.items)
            {
               xmlPrefabSet.Add(item.prefab);
            }
        }
    }

}
