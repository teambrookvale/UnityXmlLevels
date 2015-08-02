using UnityEngine;
using System.Collections;
using System.Xml;

public class DeserializedLevelsSaver
{
    public const string xmlItemsToExportGOName = "XmlItemsToExport";

    public void saveExportItems()
    {
        GameObject xmlItemsToExportGO;

        // Create XmlItemsToExport or find existing
        if (GameObject.Find(xmlItemsToExportGOName) == null)
        {
            xmlItemsToExportGO = new GameObject(xmlItemsToExportGOName);
            return;                             //we have nothing to save so skip execution
        }
        else
        {
            xmlItemsToExportGO = GameObject.Find(xmlItemsToExportGOName);
        }

        Transform[] xmlItemsToExportGOchildren = xmlItemsToExportGO.GetComponentsInChildren<Transform>();

        // Check if there isn't any Transform components except parent's
        if (xmlItemsToExportGOchildren.Length == 1)
        {
            Debug.LogError("Add the prefabs to " + xmlItemsToExportGOName);
            return;
        }

        DeserializedLevels.Level levelXml = new DeserializedLevels.Level();

        int n = 0;
        // count number of children skipping sub-items
        foreach (Transform item in xmlItemsToExportGOchildren)  //TODO replace two xmlItemsToExportGOchildren traversals with one
            if (item.parent == xmlItemsToExportGO.transform) n++;

        // the items array should have that many elements
        levelXml.items = new DeserializedLevels.Item[n];

        // use i for counting items, it would be equal (one more to be precise) to n at the end of the cycle
        int i = 0;

        // cycle through the children again and add them to items
        foreach (Transform item in xmlItemsToExportGOchildren)
        {
            // skip sub-items
            if (item.parent != xmlItemsToExportGO.transform) continue;

            levelXml.items[i] = new DeserializedLevels.Item(item);           

            // increase i for the next cycle
            i++;
        }


        // Export just one level
        DeserializedLevels levelsXmlToExport = new DeserializedLevels();
        levelsXmlToExport.levels = new DeserializedLevels.Level[1];
        levelsXmlToExport.levels[0] = levelXml;
        XmlIO.SaveXml<DeserializedLevels>(levelsXmlToExport, "./Assets/Resources/" + xmlItemsToExportGOName + ".xml");
    }

    public static string toStringNullIfZero(float num) { return num == 0 ? null : mathRound(num, 2).ToString(); }
    public static string toStringNullIfOne(float num) { return num == 1 ? null : mathRound(num, 2).ToString(); }


    public static float mathRound(float round, int decimals)
    {
        return Mathf.Round(round * Mathf.Pow(10, decimals)) / Mathf.Pow(10, decimals);
    }
}
