using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake() {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfBuildingRepairBtn");


        HideBuildingDemolishButton();
        HideBuildingRepairButton();
    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        healthSystem.OnRepaired += HealthSystem_OnRepaired;

        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnRepaired(object sender, System.EventArgs e) {
        if(healthSystem.IsFullHealth()) {
            HideBuildingRepairButton();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        ShowBuildingRepairButton();
        SoundManager.instance.PlaySound(SoundManager.Sound.BuildingDamaged, .5f);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    public void Update() {

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Instantiate(Resources.Load<Transform>("pfBuildingPlacedParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.instance.PlaySound(SoundManager.Sound.BuildingDestroyed, .5f);
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
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

    private void ShowBuildingRepairButton() {
        if (buildingRepairBtn != null) {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairButton() {
        if (buildingRepairBtn != null) {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
