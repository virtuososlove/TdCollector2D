using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyWaveUI : MonoBehaviour
{
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    [SerializeField] EnemyWaveManager enemyWaveManager;
    private RectTransform nextWaveSpawnIndicator;
    private RectTransform closestEnemyPositionIndicator;
    private Transform targetEnemyTransform = null;

    private Camera mainCamera;
    void Start()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        nextWaveSpawnIndicator = transform.Find("NextWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        closestEnemyPositionIndicator = transform.Find("ClosestEnemyPositionIndicator").GetComponent<RectTransform>();

        enemyWaveManager.onEventNumberChanged += EnemyWaveManager_onEventNumberChanged;
        mainCamera = Camera.main;
    }

    private void EnemyWaveManager_onEventNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveCounter());
    }

    void Update()
    {

        UpdateWaveMessage();
        SetWaveSpawnPositionIndicator();
        SetClosestEnemyPositionIndicator();
    }
    private void SetMessageText(string str)
    {
        waveMessageText.SetText(str);
    }
    private void SetWaveNumberText(string str)
    {
        waveNumberText.SetText(str);
    }
    private void UpdateWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer == 10)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next wave coming in  " + nextWaveSpawnTimer.ToString("F1") + "  seconds");
        }
    }
    private void SetWaveSpawnPositionIndicator()
    {
        if (Vector3.Distance(mainCamera.transform.position, enemyWaveManager.GetNextWaveSpawnPosition().position) > mainCamera.orthographicSize * 1.5f)
        {
            nextWaveSpawnIndicator.gameObject.SetActive(true);
            Vector3 dirToSpawnPosition = (enemyWaveManager.GetNextWaveSpawnPosition().position - mainCamera.transform.position).normalized;
            nextWaveSpawnIndicator.anchoredPosition = 300f * dirToSpawnPosition;
            nextWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.FromVectorToDegree(dirToSpawnPosition));
        }
        else
        {
            nextWaveSpawnIndicator.gameObject.SetActive(false);
        }
    }
    private void SetClosestEnemyPositionIndicator()
    {
        float maxtargetarea = 9999;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, maxtargetarea);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemyTransform == null)
                {
                    targetEnemyTransform = enemy.transform;

                }
                else
                {
                    if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(targetEnemyTransform.position, transform.position))
                    {
                        targetEnemyTransform = enemy.transform;
                    }
                }
            }
        }
        if (targetEnemyTransform != null)
        {
            if (Vector3.Distance(mainCamera.transform.position, targetEnemyTransform.position) > mainCamera.orthographicSize * 1.5f)
            {
                closestEnemyPositionIndicator.gameObject.SetActive(true);
                Vector3 dirToClosestEnemyPosition = (targetEnemyTransform.position - mainCamera.transform.position).normalized;
                closestEnemyPositionIndicator.anchoredPosition = 175f * dirToClosestEnemyPosition;
                closestEnemyPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.FromVectorToDegree(dirToClosestEnemyPosition));
            }
            else
            {
                closestEnemyPositionIndicator.gameObject.SetActive(false);
            }
        }
    }
}
