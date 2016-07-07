using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour
{

    void Start()
    {
        DeserializedLevelsLoader d = new DeserializedLevelsLoader();
        d.generateItems();
    }

}
