using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager Instance { get; private set; }


    public event EventHandler OnResourceAmountChange;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake() {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        //Debug.Log("Resources:  " + Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name));

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceDictionary();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.list[0], 2);
            TestLogResourceDictionary();
        }

    }

    private void TestLogResourceDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.resourceName + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChange?.Invoke(this, EventArgs.Empty);

        //if(OnResourceAmountChange != null) {
        //    OnResourceAmountChange(this, EventArgs.Empty);
        //}
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray, out string errorMessage) {
        foreach(ResourceAmount resourceAmount in resourceAmountArray) {
            if(GetResourceAmount(resourceAmount.resourceType)>= resourceAmount.amount) {
                // Can afford this.
            }else {
                //Cannot afford this.
                errorMessage = "Insufficient funds.";
                return false;
            }
        }
        errorMessage = "";
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
            
    }
}
