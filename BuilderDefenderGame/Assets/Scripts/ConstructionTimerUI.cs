using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{

    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image construnctionProgressImage;

    private void Awake() {
        construnctionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update() {
        construnctionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
