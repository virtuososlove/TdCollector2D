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
    void Start()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveManager.onEventNumberChanged += EnemyWaveManager_onEventNumberChanged;
    }

    private void EnemyWaveManager_onEventNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveCounter());
    }

    void Update()
    {
       
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if(nextWaveSpawnTimer == 10)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next wave coming in  " + nextWaveSpawnTimer.ToString("F1") + "  seconds");
        }
    }
    private void SetMessageText(string str)
    {
        waveMessageText.SetText(str);
    }
    private void SetWaveNumberText(string str)
    {
        waveNumberText.SetText(str);
    }
}
