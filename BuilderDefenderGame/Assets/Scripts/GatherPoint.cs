using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherPoint : MonoBehaviour {

    //[SerializeField] private Transform gatherPointSprite;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Vector3 mousePos = Input.mousePosition + new Vector3(0,0,10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            gameObject.transform.position = worldPosition;
        }
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Wwww is :" + Input.mousePosition);
        };
    }

}
