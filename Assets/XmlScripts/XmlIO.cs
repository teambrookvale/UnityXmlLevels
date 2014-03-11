using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine; // necessary for TextAsset

public static class XmlIO
{
	public static void SaveXml<T> (this object deserializedXml, string path) where T : class
	{
		using(var stream = new FileStream(path, FileMode.Create))
		{
			var s = new XmlSerializer(typeof(T));
			s.Serialize(stream, deserializedXml);
		}
	}

	public static T LoadXml<T>(string textAssetName) where T : class
	{
		TextAsset xmlTextAsset = (TextAsset) Resources.Load (textAssetName, typeof(TextAsset));

		using(var stream = new StringReader(xmlTextAsset.text))
		{
			var s = new XmlSerializer(typeof(T));
			T deserializedXml = s.Deserialize(stream) as T;
			return deserializedXml;
		}
	}

}
