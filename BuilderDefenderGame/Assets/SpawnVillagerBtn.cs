using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnVillagerBtn : MonoBehaviour
{
    [SerializeField] private GameObject villagerPrefab;


    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("SpawnVillager!");
        });
    }
}
