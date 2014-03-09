using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine; // necessary for TextAsset

public class XmlIO
{
	static XmlSerializer ser;

	public static void Save(string path, DeserializedLevels levelsXml)
	{
		using(var stream = new FileStream(path, FileMode.Create))
		{
			initSerializer();
			ser.Serialize(stream, levelsXml);
		}
	}


	public static DeserializedLevels LoadLevels()
	{
		TextAsset xmlTextAsset = (TextAsset) Resources.Load ("Levels", typeof(TextAsset));

		using(var stream = new StringReader(xmlTextAsset.text))
		{
			initSerializer();
			DeserializedLevels deserializedLevelXml = ser.Deserialize(stream) as DeserializedLevels;
			return deserializedLevelXml;
		}
	}

	private static void initSerializer () {	if (ser == null) ser = new XmlSerializer(typeof(DeserializedLevels)); }
}
