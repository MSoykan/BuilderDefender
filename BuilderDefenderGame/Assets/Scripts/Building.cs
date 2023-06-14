using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishBtn;

    private void Awake() {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        if(buildingDemolishBtn != null) {
        buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthSystem.OnDied += HealthSystem_OnDied;
    }

    public void Update() {

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    private void OnMouseEnter() {
        ShowBuildingDemolishButton();
    }
    private void OnMouseExit() {
        HideBuildingDemolishButton();
    }

    private void ShowBuildingDemolishButton() {
        if (buildingDemolishBtn != null) {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishButton() {
        if (buildingDemolishBtn != null) {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}
