using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Trying to repair! CLicked button.");
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount(); ;
            int repairCost = missingHealth / 3;

            ResourceAmount[] resourceAmount = new ResourceAmount[] {
                new ResourceAmount { resourceType = goldResourceType, amount = repairCost }, };
            Debug.Log("Created array: +"+ resourceAmount);

            if (ResourceManager.Instance.CanAfford(resourceAmount, out string errorMessage)) {
                //Can afford repairs
                Debug.Log("Can afford repairs!");
                ResourceManager.Instance.SpendResources(resourceAmount);
                healthSystem.HealFull();
            }
            else {
                //Cannot afford repairs!
                Debug.Log("cannot afford repairs!");
                TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }
}
