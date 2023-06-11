using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass {
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition() {
        if(mainCamera == null) {
            mainCamera = Camera.main;
            Debug.Log("s�rekli null");
        }
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
