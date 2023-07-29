using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour {


    public static CameraHandler Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float moveSpeed = 30f;
    private float zoomOut = 2f;
    private float minOrthographicSize = 10f;
    private float maxOrthographicSize = 30f;
    public float orthographicSize;
    public float targetOrthographicSize;
    private bool edgeScrolling;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update() {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        if (edgeScrolling) {
            float edgeScrollingSize = 30;
            if (Input.mousePosition.x > (Screen.width - edgeScrollingSize)) {
                x = +1f;
            }
            if (Input.mousePosition.x < edgeScrollingSize) {
                x = -1f;
            }
            if (Input.mousePosition.y > (Screen.height - edgeScrollingSize)) {
                y = +1f;
            }
            if (Input.mousePosition.y < edgeScrollingSize) {
                y = -1f;
            }
        }

        Vector2 moveDir = new Vector2(x, y).normalized;
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom() {
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomOut;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void SetEdgeScrolling(bool edgeScrolling) {
        this.edgeScrolling = edgeScrolling;
    }

    public bool GetEdgeScrolling() {
        return edgeScrolling;
    }
}
