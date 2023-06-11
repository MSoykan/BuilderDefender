using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        Debug.Log("Buildings: " + Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name));
        activeBuildingType = buildingTypeList.list[0];
    }

    private void Start() {
        mainCamera = Camera.main;

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }

        //if (Input.GetKeyDown(KeyCode.T)) {
        //    activeBuildingType = buildingTypeList.list[0];
        //}
        //if (Input.GetKeyDown(KeyCode.Y)) {
        //    activeBuildingType = buildingTypeList.list[1];
        //}
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }
}
