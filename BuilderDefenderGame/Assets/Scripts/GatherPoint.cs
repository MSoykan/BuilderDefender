using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherPoint : MonoBehaviour {

    [SerializeField] private Transform gatherPointSprite;

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            gatherPointSprite.transform.position = Input.mousePosition;
        }
    }

}
