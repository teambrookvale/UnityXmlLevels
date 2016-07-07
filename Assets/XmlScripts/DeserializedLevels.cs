using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Levels")]
public class DeserializedLevels
{
    [XmlElement("Developer")]
    public Developer developer;
    public class Developer
    {
        [XmlAttribute("StartLevel")]
        public string startLevel;
    }

    [XmlElement("Level")]
    public Level[] levels;
    public class Level
    {
        [XmlAttribute("playerx")]
        public string playerx;

        [XmlAttribute("playery")]
        public string playery;

        [XmlAttribute("playerrot")]
        public string playerrot;

        [XmlElement("Item")]
        public Item[] items;
    }

    public class Item
    {
        [XmlAttribute("prefab")]
        public string prefab;

        [XmlAttribute("x")]
        public string x;

        [XmlAttribute("y")]
        public string y;

        [XmlAttribute("rot")]
        public string rot;

        [XmlAttribute("scale_x")]
        public string scale_x;

        [XmlAttribute("scale_y")]
        public string scale_y;

        public Item() { }

        public Item(Transform item)
        {
            prefab = item.name;
            x = DeserializedLevelsSaver.toStringNullIfZero(item.transform.position.x);
            y = DeserializedLevelsSaver.toStringNullIfZero(item.transform.position.y);
            rot = DeserializedLevelsSaver.toStringNullIfZero(item.localRotation.eulerAngles.x);
            scale_x = DeserializedLevelsSaver.toStringNullIfOne(item.localScale.x);
            scale_y = DeserializedLevelsSaver.toStringNullIfOne(item.localScale.y);
        }
    }
}

