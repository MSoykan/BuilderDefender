using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour {
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnPositionIndicator;
    private Camera mainCamera;
    private RectTransform closestEnemyPositionIndicator;

    private void Awake() {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        closestEnemyPositionIndicator = transform.Find("closestEnemyPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start() {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());

    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e) {
        Debug.Log("Event triggered");
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    public void Update() {

        HandleNextWaveMessage();
        HandleEnemySpawnPositionIndicator();
        HandleClosestEnemyIndicator();

    }

    private void HandleNextWaveMessage() {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer < 0f) {
            SetMessageText("");
        }
        else {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleClosestEnemyIndicator() {
        Enemy closestEnemy = LookForClosestEnemy();
        if (closestEnemy != null) {
            Vector3 dirToClosestEnemy = (closestEnemy.transform.position - mainCamera.transform.position).normalized;
            closestEnemyPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;

            closestEnemyPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));
        }
        else {
            //No enemies alive
            closestEnemyPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void HandleEnemySpawnPositionIndicator() {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;

        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));
    }

    private void SetMessageText(string message) {
        waveNumberText.text = message;
    }

    private void SetWaveNumberText(string text) {
        waveMessageText.SetText(text);
    }

    private Enemy LookForClosestEnemy() {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D collider2D in collider2DArray) {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null) {
                //It's a building 
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                }
                else {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position)) {
                        //Closer!
                        targetEnemy = enemy;
                    }
                }
            }
        }
        return targetEnemy;
    }
}
