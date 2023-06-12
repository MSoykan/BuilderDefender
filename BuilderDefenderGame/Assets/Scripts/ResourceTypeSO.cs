using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject {

    public Sprite sprite;
    public string nameShort;
    public string resourceName;
    public string colorHex;
}
