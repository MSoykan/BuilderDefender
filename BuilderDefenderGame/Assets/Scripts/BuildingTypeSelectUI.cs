using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField] private Sprite arrowSprite;

    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private Transform arrowButton;
    private void Awake() {
        Transform btnTemplate = transform.Find("buttonTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        //Now lets create the first UI button(arrowButton)
        int index = 0;
        arrowButton = Instantiate(btnTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offSetAmount = 120;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40 + offSetAmount * index, +30);

        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });


        Debug.Log("Index :: " + index);
        index++;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            offSetAmount = 120;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(40 + offSetAmount * index, +30);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            btnTransform.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0,-30);

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
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys) {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null) {
            arrowButton.Find("selected").gameObject.SetActive(true);
        }
        else {
            btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
