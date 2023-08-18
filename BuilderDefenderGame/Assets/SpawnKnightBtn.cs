using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKnightBtn : MonoBehaviour
{
    [SerializeField] private GameObject villagerPrefab;
    [SerializeField] private Transform spawnTransform;


    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("SpawnKnight!!!");

            Instantiate(villagerPrefab, spawnTransform.position, Quaternion.identity);
        });
    }
}
