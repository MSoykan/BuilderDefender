using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIButtonInformation", menuName = "UIButtonInformation")]
public class ButtonUIInformationSO : ScriptableObject {
    //This code is only for filling in the UI neccesities
    public GameObject prefabToInstantiate; // The prefab will have the neccesary information code in it for other func

    public ResourceAmount[] costResourceAmounts;

    public Sprite buttonSprite;

    public string unitName; // TODO: change this

}   
