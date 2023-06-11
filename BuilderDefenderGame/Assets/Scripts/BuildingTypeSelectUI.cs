using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private void Awake() {
        Transform btnTemplate = transform.Find("buttonTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            float offSetAmount = 120;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(40 + offSetAmount * index, +30);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            btnTransformDictionary[buildingType] = btnTransform;

            Debug.Log("Index :: " + index);
            index++;
        }
    }

    private void Update() {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys) {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
    }
}
