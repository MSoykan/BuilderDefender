using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnitButton : MonoBehaviour {

    [SerializeField] private ButtonUIInformationSO buttonUIInformation;
    [SerializeField] private Transform spawnPoint;

    public Image buttonImage;


    private void Awake() {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Spawning :: "+ buttonUIInformation.unitName);

            GameObject unitPrefab = Instantiate(buttonUIInformation.prefabToInstantiate, spawnPoint.position, Quaternion.identity);
            Unit unit = unitPrefab.GetComponent<Unit>();
            unit.targetTransform = spawnPoint;
        });
        
    }

    private void Start() {
        buttonImage.sprite = buttonUIInformation.buttonSprite;
    }




    private void OnMouseEnter() {
        //TODO: make a hover over window to display the cost of it
    }
}
