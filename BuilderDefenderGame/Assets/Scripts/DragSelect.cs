using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelect : MonoBehaviour {

    private bool isDragSelect = false;

    private Vector3 mousePositionInitial;
    private Vector3 mousePositionEnd;

    public RectTransform selectionBox;

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            mousePositionInitial = Input.mousePosition;
            isDragSelect = false;
        }

        if (Input.GetMouseButton(0)) {
            if (!isDragSelect && (mousePositionInitial - Input.mousePosition).magnitude > 30) {
                isDragSelect = true;
            }
            if (isDragSelect) {
                mousePositionEnd = Input.mousePosition;
                UpdateSelectionBox();
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (isDragSelect) {
                isDragSelect = false;
                UpdateSelectionBox();
            }
        }
    }

    private void UpdateSelectionBox() {
        selectionBox.gameObject.SetActive(isDragSelect);

        float width = mousePositionEnd.x - mousePositionInitial.x;
        float height = mousePositionEnd.y - mousePositionInitial.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        selectionBox.anchoredPosition = new Vector2(mousePositionInitial.x, mousePositionInitial.y) + new Vector2(width / 2, height / 2);
    }

}
