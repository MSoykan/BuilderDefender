using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;
    private Transform seperatorContainer;

    private void Awake() {
        barTransform = transform.Find("bar");
        seperatorContainer = transform.Find("seperatorContainer");

    }

    private void Start() {

        ConstructHealthBarSeperators();

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnRepaired += HealthSystem_OnRepaired;
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e) {
        throw new System.NotImplementedException();
    }

    private void ConstructHealthBarSeperators() {
        Transform seperatorTemplate = seperatorContainer.Find("seperatorTemplate");
        seperatorTemplate.gameObject.SetActive(false);

        foreach (Transform seperatorTransform in seperatorContainer) {
            if (seperatorTransform == seperatorTemplate) continue;
            Destroy(seperatorTransform.gameObject);
        }

        int healthAmountPerSeperator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeperatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeperator);

        for (int i = 0; i < healthSeperatorCount; i++) {
            Transform seperatorTransform = Instantiate(seperatorTemplate, seperatorContainer);
            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeperator, 0, 0);
        }
    }

    private void HealthSystem_OnRepaired(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible() {
        if (healthSystem.IsFullHealth()) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
        gameObject.SetActive(true);

    }
}
